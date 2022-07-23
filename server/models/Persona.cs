using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    internal class Persona
    {
        public Persona()
        {
            results = new List<Result>();
            info = new Info();
        }

        public List<Result> results { get; set; }
        public Info info { get; set; }
    }

    internal class Result
    {
        public Result()
        {
            name = new Name();
            location = new Location();
            login = new Login();
            dob = new Dob();
            registered = new Registered();
            id = new Id();
            picture = new Picture();
        }
        public string gender{ get; set; }
        public Name name { get; set; }
        public Location location { get; set; }
        public string email { get; set; }
        public Login login { get; set; }
        public Dob dob { get; set; }
        public Registered registered { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }
        public Id id { get; set; }
        public Picture picture { get; set; }
        public string nat { get; set; }
    }

    internal class Picture
    {
        public string large { get; set; }
        public string medium { get; set; }
        public string thumbnail { get; set; }
    }

    internal class Id
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    internal class Registered
    {
        public string date { get; set; }
        public string age { get; set; }
    }

    internal class Dob
    {
        public string date { get; set; }
        public string age { get; set; }
    }

    internal class Login
    {
        public string uuid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string md5 { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }
    }

    internal class Name
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    internal class Location
    {
        public Location()
        {
            street = new Street();
            coordinates = new Coordinates();
            timezone = new TimeZone();
        }
        public Street street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }
        public Coordinates coordinates { get; set; }
        public TimeZone timezone { get; set; }
    }

    internal class Street
    {
        public string number { get; set; }
        public string name { get; set; }
    }

    internal class Coordinates
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    internal class TimeZone
    {
        public string offset { get; set; }
        public string description { get; set; }
    }

    internal class Info
    {
        public string seed { get; set; }
        public string results { get; set; }
        public string page { get; set; }
        public string version { get; set; }
    }
}