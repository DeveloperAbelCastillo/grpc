@echo off
SERVER_CN=localhost
openssl genrsa -passout pass:1111 -des3 -out server.key 4096
set /p asd="Presione para Continuar"
openssl req -passin pass:1111 -new -key server.key -out server.csr -subj  "//CN=${SERVER_CN}"
set /p asd="Presione para Continuar"
openssl x509 -passin pass:1111 -req -days 365 -in server.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out diferencia.crt
set /p asd="Presione para Continuar"
openssl rsa -passin pass:1111 -in server.key -out server.key
set /p asd="Presione para Continuar"