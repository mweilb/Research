using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.OrleansInterfaces
{
  
    public class FeedbackMessage
    {
        public enum Responces { None, Error };

        public FeedbackMessage(Responces type= Responces.None, string value ="") { mType = type; mStringValue = value; }
        public Responces mType { get; set; }
        public string mStringValue { get; set; } 
    }
}
