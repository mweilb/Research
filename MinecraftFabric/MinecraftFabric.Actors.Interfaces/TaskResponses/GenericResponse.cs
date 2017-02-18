using System.Runtime.Serialization;

namespace MinecraftFabric.Actors.Interfaces
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
