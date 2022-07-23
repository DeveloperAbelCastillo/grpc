@echo off
SERVER_CN=localhost
openssl genrsa -passout pass:1111 -des3 -out client.key 4096
set /p asd="Presione para Continuar"
openssl req -passin pass:1111 -new -key client.key -out client.csr -subj  "//CN=${SERVER_CN}"
set /p asd="Presione para Continuar"
openssl x509 -passin pass:1111 -req -days 365 -in client.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out client.crt
set /p asd="Presione para Continuar"