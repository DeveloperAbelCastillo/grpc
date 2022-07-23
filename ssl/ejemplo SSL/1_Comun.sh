@echo off
SERVER_CN=localhost
openssl genrsa -passout pass:1111 -des3 -out ca.key 4096
set /p asd="Presione para Continuar"
openssl req -passin pass:1111 -new -x509 -days 365 -key ca.key -out ca.crt -subj  "//CN=MyRootCA"
set /p asd="Presione para Continuar"