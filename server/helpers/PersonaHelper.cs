using Ejemplo;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace helpers
{
    internal class PersonaHelper
    {
        public static async Task<Persona[]> GenerarListaPersonas(int cantidad)
        {
            try
            {
                //Consume https://random-data-api.com/api/
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://randomuser.me/api/?results=" + cantidad.ToString());
                var json = await response.Content.ReadAsStringAsync();
                var abc = JsonConvert.DeserializeObject(json);
                var personasResult = JsonConvert.DeserializeObject<models.Persona>(json);
                return await GenerarPersona(personasResult);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static Task<Persona[]> GenerarPersona(models.Persona personasResult)
        {
            var personas = new Persona[personasResult.results.Count];

            try
            {
                //Parsear datos
                //Recorrer lista
                for (int i = 0; i < personasResult.results.Count; i++)
                {
                    personas[i] = new Persona()
                    {
                        Uuid = personasResult.results[i].login.uuid,
                        Usuario = personasResult.results[i].login.username,
                        Clave = personasResult.results[i].login.password,
                        Nombre = personasResult.results[i].name.first,
                        ApellidoPaterno = personasResult.results[i].name.last,
                        ApellidoMaterno = "",
                        Email = personasResult.results[i].email,
                        Avatar = personasResult.results[i].picture.medium,
                        Genero = personasResult.results[i].gender,
                        Telefono = personasResult.results[i].phone,
                        NSS = string.IsNullOrEmpty(personasResult.results[i].id.value) ? string.Empty : personasResult.results[i].id.value,
                        FechaNacimiento = personasResult.results[i].dob.date
                    };
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return Task.FromResult(personas);
        }
    }
}
