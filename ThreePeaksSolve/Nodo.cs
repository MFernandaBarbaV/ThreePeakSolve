using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ThreePeaksSolve
{
    public class Nodo
    {
        public Pico Pico { get; set; }

        public Nodo NodoPadre { get; set; }

        public List<Nodo> NodosHijos { get; set; }
               
        public Nodo(Pico pico)
        {
            NodosHijos = new List<Nodo>();
            this.Pico = pico;
        }

        public Nodo(Carta carta)
        {
            NodosHijos = new List<Nodo>();           
            this.Pico = new Pico(carta, false);
        }

        public override string ToString()
        {
            return Pico.ToString() ;
        }
               
    }
}
