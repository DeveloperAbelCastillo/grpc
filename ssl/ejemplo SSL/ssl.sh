@echo off
SERVER_CN=localhost
#set OPENSSL_CONF=c:\OpenSSL-Win64\bin\openssl.cfg
set OPENSSL_CONF=C:\Program Files\OpenSSL-Win64\bin\openssl.cfg

#Generamos la llave común
echo Generar llave ca.key:
openssl genrsa -passout pass:1111 -des3 -out ca.key 4096

#Generamos el certificado común
echo Generar certificado ca.crt:
openssl req -passin pass:1111 -new -x509 -days 365 -key ca.key -out ca.crt -subj  "//CN=MyRootCA"

#Generar llave para el servidor
echo Generar llave server.key:
openssl genrsa -passout pass:1111 -des3 -out server.key 4096

#Generar el certificado para el servidor
echo Generar solicitud de firma del servidor server.csr:
openssl req -passin pass:1111 -new -key server.key -out server.csr -subj  "//CN=${SERVER_CN}"

#Generar el certificado para el autofirmado
echo Certificado de servidor de autofirma server.crt:
openssl x509 -passin pass:1111 -req -days 365 -in server.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out server.crt

#Generar el certificado para el autofirmado
echo Eliminar la frase de contraseña de la clave del servidor:
openssl rsa -passin pass:1111 -in server.key -out server.key

#Generar llave para el cliente
echo Generar llave client.key:
openssl genrsa -passout pass:1111 -des3 -out client.key 4096

#Generar el certificado para el cliente
echo Generar solicitud de firma del cliente client.csr:
openssl req -passin pass:1111 -new -key client.key -out client.csr -subj  "//CN=${SERVER_CN}"

#Generar el certificado para el autofirmado
echo Certificado de cliente de autofirma client.crt:
openssl x509 -passin pass:1111 -req -days 365 -in client.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out client.crt

echo Eliminar la frase de contraseña de la clave del cliente:
openssl rsa -passin pass:1111 -in client.key -out client.key

cp ca.crt ./server/ca.crt
cp ca.crt ./client/ca.crt

cp server.crt ./server/server.crt
cp server.key ./server/server.key

cp client.crt ./client/client.crt
cp client.key ./client/client.key