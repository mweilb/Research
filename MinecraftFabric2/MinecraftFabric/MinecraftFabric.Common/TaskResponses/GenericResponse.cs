using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftFabric.ActorServices.TaskResponses
{
    [DataContract]
    public class GenericResponse
    {
        [DataMember]
        public bool success = false;
        [DataMember]
        public string message = "";

        public GenericResponse()
        {

        }

        public GenericResponse(bool _success, string _message)
        {
            success = _success;
            message = _message;
        }
    }
}
