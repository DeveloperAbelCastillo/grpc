using System;
using System.Threading.Tasks;
using Grpc.Core;
using System.Linq;
using static Ejemplo.EjemploService;
using Ejemplo;
using helpers;

namespace server
{
    public class EjemploServiceImpl : EjemploServiceBase
    {
        //public override async Task<SaludoResponse> Saludar(SaludoRequest request, ServerCallContext context)
        //{
        //    await Task.Delay(300);

        //    Console.WriteLine("Se realizo una peticion al servicio saludar_con_fecha_limite");

        //    return new SaludoResponse() { Result = String.Format("Hola {0} {1}", request.Persona.FirstName, request.Persona.LastName) };
        //}

        //Metodo para devolver lista de personas
        public override async Task GenerarListaPersonas(PersonasCantidadRequest request, IServerStreamWriter<PersonaResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("Se realizo una peticion al servicio get_personas");
            
            await Task.Delay(300);

            try
            {
                var result = await PersonaHelper.GenerarListaPersonas(request.Cantidad);
            
                //recorrer result yh devolver por strem
                foreach (Persona persona in result)
                {
                    await responseStream.WriteAsync(new PersonaResponse() { Persona = persona });
                    Console.WriteLine("Se envia la Uuid: " + persona.Uuid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
