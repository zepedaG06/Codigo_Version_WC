using System.Collections.Generic;

namespace MEDICENTER
{
    public class DecisionNode
    {
        public string Id { get; set; }
        public string Pregunta { get; set; }
        public string Diagnostico { get; set; }
        public List<DecisionNode> Hijos { get; set; }
        public string RespuestaEsperada { get; set; }

        public DecisionNode(string id, string pregunta)
        {
            Id = id;
            Pregunta = pregunta;
            Hijos = new List<DecisionNode>();
        }

        public DecisionNode(string id, string diagnostico, bool esHoja)
        {
            Id = id;
            Diagnostico = diagnostico;
            Hijos = new List<DecisionNode>();
        }

        public bool EsHoja()
        {
            return !string.IsNullOrEmpty(Diagnostico) && Hijos.Count == 0;
        }

        public void AgregarHijo(DecisionNode hijo)
        {
            Hijos.Add(hijo);
        }
    }
}