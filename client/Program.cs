using Ejemplo;
using Grpc.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        const string target = "127.0.0.1:50051";

        static async Task Main(string[] args)
        {
            var clientcert = File.ReadAllText("ssl/client.crt");
            var clientkey = File.ReadAllText("ssl/client.key");
            var cacert = File.ReadAllText("ssl/ca.crt");
            var channelCredentials = new SslCredentials(cacert, new KeyCertificatePair(clientcert, clientkey));
            Channel channel = new Channel("localhost", 50051, channelCredentials);

            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("El cliente se conectó exitosamente");
            });

            var client = new EjemploService.EjemploServiceClient(channel);

            try
            {
                //await Saludar(client);
                await ObtenerListaPersonas(client);

            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
            {
                Console.WriteLine("Error : " + ex.Status.Detail);
            }

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }

        //public static Task Saludar(EjemploService.EjemploServiceClient client)
        //{
        //    var persona = new Persona()
        //    {
        //        Id = "1",
        //        Uid = "c48e0afa-093a-4018-8937-e0f2666d868f",
        //        Password = "1NUAksUZGrRocZeqo1Cr",
        //        FirstName = "Abel",
        //        LastName = "Castillo",
        //        Username = "acastillo",
        //        Email = "acastillo@email.com",
        //        Avatar = "https://robohash.org/consequaturcommodiet.png?size=300x300&set=set1",
        //        Gender = "Male",
        //        PhoneNumber = "+52 331.000.9999",
        //        SocialInsuranceNumber = "5135813884",
        //        DateOfBirth = "1988-05-14",
        //    };

        //    var responseSaludo = client.Saludar(new SaludoRequest() { Persona = persona }, deadline: DateTime.UtcNow.AddSeconds(5));

        //    Console.WriteLine(responseSaludo.Result);
        //    return Task.CompletedTask;
        //}

        public static async Task ObtenerListaPersonas(EjemploService.EjemploServiceClient client)
        {
            var request = new PersonasCantidadRequest() { Cantidad =  5000};
            var response = client.GenerarListaPersonas(request);
            int contador = 1;

            while (await response.ResponseStream.MoveNext())
            {
                var persona = response.ResponseStream.Current.Persona;
                Console.WriteLine("**********************************************************");
                Console.WriteLine("*****   Registro No.: " + contador++);
                Console.WriteLine("**********************************************************");
                Console.WriteLine("Uuid: " + persona.Uuid);
                Console.WriteLine("Usuario: " + persona.Usuario);
                Console.WriteLine("Clave: " + persona.Clave);
                Console.WriteLine("Nombre: " + persona.Nombre);
                Console.WriteLine("ApellidoPaterno: " + persona.ApellidoPaterno);
                Console.WriteLine("ApellidoMaterno: " + persona.ApellidoMaterno);
                Console.WriteLine("Email: " + persona.Email);
                Console.WriteLine("Avatar: " + persona.Avatar);
                Console.WriteLine("Genero: " + persona.Genero);
                Console.WriteLine("Telefono: " + persona.Telefono);
                Console.WriteLine("NSS: " + persona.NSS);
                Console.WriteLine("FechaNacimiento: " + persona.FechaNacimiento);
            }
        }
    }
}
