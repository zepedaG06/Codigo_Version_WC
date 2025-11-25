using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    public class DecisionNode
    {
        public string Id;
        public string Pregunta;
        public string Diagnostico;
        public List<DecisionNode> Hijos;
        public string RespuestaEsperada;

        public DecisionNode(string nodoId, string nodoPregunta)
        {
            Id = nodoId;
            Pregunta = nodoPregunta;
            Hijos = new List<DecisionNode>();
        }

        public DecisionNode(string nodoId, string nodoDiagnostico, bool esHoja)
        {
            Id = nodoId;
            Diagnostico = nodoDiagnostico;
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