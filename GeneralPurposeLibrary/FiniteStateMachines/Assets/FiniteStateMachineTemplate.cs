using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeneralPurposeLibrary.FiniteStateMachines.Assets
{
    [XmlRoot(ElementName = "Transition")]
    public class Transition
    {
        [XmlAttribute(AttributeName = "CurrentState")]
        public string CurrentState { get; set; }

        [XmlAttribute(AttributeName = "Event")]
        public string Event { get; set; }

        [XmlAttribute(AttributeName = "NextState")]
        public string NextState { get; set; }
    }

    [XmlRoot(ElementName = "Transitions")]
    public class Transitions
    {
        [XmlElement(ElementName = "Transition")]
        public List<Transition> Transition { get; set; }
    }
}