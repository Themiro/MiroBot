using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

//=====================//
//                     //
//    Legacy Code      //
//    (Unused now)     //
//                     //
//=====================//

namespace _23012021Rewrite.Modules
{
    public class StateAndConfig
    {
        string _status;
        public StateAndConfig()
        {}
        public string status
        {
            set
            {
                _status = value;
            }
            get
            {
                return _status;
            }
        }
    }
    
    [Serializable]
    [XmlInclude(typeof(StateAndConfig))]
    public class StateSavingReading
    {
        public StateSavingReading()
        {}
        public void OldSave(string Location, StateAndConfig stateAndConfig)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(StateAndConfig));
            System.IO.FileStream file = System.IO.File.Create(Location);
            writer.Serialize(file, stateAndConfig);
            file.Close();
        }
        public StateAndConfig OldOpen(string Location)
        {
            StateAndConfig DataPool;
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(ArrayList));
            System.IO.StreamReader file = new System.IO.StreamReader(Location);
            DataPool = (StateAndConfig)reader.Deserialize(file);
            file.Close();
            return DataPool;
        }
    }
}
