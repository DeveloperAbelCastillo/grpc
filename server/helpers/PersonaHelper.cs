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
            //Consume https://random-data-api.com/api/
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://randomuser.me/api/?results=" + cantidad.ToString());
            var json = await response.Content.ReadAsStringAsync();
            var abc = JsonConvert.DeserializeObject(json);
            var personasResult = JsonConvert.DeserializeObject<models.Persona>(json);
            return await GenerarPersona(personasResult);
        }

        public static Task<Persona[]> GenerarPersona(models.Persona personasResult)
        {
            //Parsear datos
            var personas = new Persona[personasResult.results.Count];
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
                    NSS = personasResult.results[i].id.value,
                    FechaNacimiento = personasResult.results[i].dob.date
                };
            }
            return Task.FromResult(personas);

        }
    }
}
