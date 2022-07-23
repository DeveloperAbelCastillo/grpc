using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using Ejemplo;

namespace server
{
    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
            Server server = null;

            try
            {
                var keypair = new KeyCertificatePair(File.ReadAllText("ssl/server.crt"), File.ReadAllText("ssl/server.key"));
                var cacert = File.ReadAllText("ssl/ca.crt");
                var credentials = new SslServerCredentials(new List<KeyCertificatePair>() { keypair }, cacert, false);

                server = new Server()
                {
                    Services = { EjemploService.BindService(new EjemploServiceImpl()) },
                    Ports = { new ServerPort("localhost", Port, credentials) }
                };

                server.Start();
                Console.WriteLine("El servidor está escuchando en el puerto : " + Port);
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine("El servidor no pudo iniciarse : " + e.Message);
                throw;
            }
            finally
            {
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}
