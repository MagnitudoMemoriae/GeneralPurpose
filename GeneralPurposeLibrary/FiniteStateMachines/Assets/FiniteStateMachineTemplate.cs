using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeneralPurposeLibrary.FiniteStateMachines.Assets
{
    [XmlRoot(ElementName = "XmlFSMState")]
    public class XmlFSMState
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "XmlFSMCondition")]
    public class XmlFSMCondition
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "XmlListOfCondition")]
    public class XmlListOfCondition
    {
        [XmlElement(ElementName = "XmlFSMCondition")]
        public List<XmlFSMCondition> XmlFSMCondition { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "XmlFSMConditions")]
    public class XmlFSMConditions
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "ListOfCondition")]
        public string ListOfCondition { get; set; }

        [XmlAttribute(AttributeName = "Operation")]
        public string Operation { get; set; }
    }

    [XmlRoot(ElementName = "XmlFSMEvent")]
    public class XmlFSMEvent
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Conditions")]
        public string Conditions { get; set; }
    }

    [XmlRoot(ElementName = "Xml")]
    public class Xml
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Conditions")]
        public string Conditions { get; set; }
    }

    [XmlRoot(ElementName = "XmlTransition")]
    public class XmlTransition
    {
        [XmlAttribute(AttributeName = "CurrentState")]
        public string CurrentState { get; set; }

        [XmlAttribute(AttributeName = "Event")]
        public string Event { get; set; }

        [XmlAttribute(AttributeName = "NextState")]
        public string NextState { get; set; }
    }

    [XmlRoot(ElementName = "XmlFiniteStateMachine")]
    public class XmlFiniteStateMachine
    {
        [XmlElement(ElementName = "XmlFSMState")]
        public List<XmlFSMState> XmlFSMState { get; set; }

        [XmlElement(ElementName = "XmlListOfCondition")]
        public List<XmlListOfCondition> XmlListOfCondition { get; set; }

        [XmlElement(ElementName = "XmlFSMConditions")]
        public List<XmlFSMConditions> XmlFSMConditions { get; set; }

        [XmlElement(ElementName = "XmlFSMEvent")]
        public XmlFSMEvent XmlFSMEvent { get; set; }

        [XmlElement(ElementName = "Xml")]
        public Xml Xml { get; set; }

        [XmlElement(ElementName = "XmlTransition")]
        public List<XmlTransition> XmlTransition { get; set; }
    }
}