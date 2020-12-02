using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.CommonTools
{
    //Session'i Extension haline getirmemizin nedeni kompleks tiplerimizi alması gerektigindendir...Extension metotlar sadece generic olmayan static class'larda tanımlanabilir...
    public static  class SessionExtension
    {
        //Session'imizi belirleyecek metodu yaratıyoruz

        public static void SetObject(this ISession session,string key,object value)
        {
            string objectString = JsonConvert.SerializeObject(value);
            session.SetString(key, objectString);
        }


        //Session'i geri almak lazım...Generic metotlar

        //User, Profile , 12, "asd"

        //Araba sınıfı   b.SetObject("araba",a)

        // GetObject<Araba>("araba")
        public static T GetObject<T>(this ISession session,string key) where T:class //(T bir referans tip olmak zorundadır)
        {
            string objectString = session.GetString(key);
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            T deserializedObject = JsonConvert.DeserializeObject<T>(objectString);
            return deserializedObject;
        }


    }
}
