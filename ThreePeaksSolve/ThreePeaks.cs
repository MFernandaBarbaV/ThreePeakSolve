using System;
using System.Collections.Generic;
using System.Linq;

namespace ThreePeaksSolve
{
    public static class ThreePeaks
    {
        private static bool ContinuarBuscando { get; set; }
        private static int MaxIntentosExitosos { get { return 1000; } }
        private static int MaxIntentosNoExitosos { get { return 15000; } }
        private static int ConteoIntentosExitosos { get; set; }
        public static int ConteoIntentosNoExitosos { get; set; }

        private static List<Tuple<int, List<Carta>>> nodosGanadores = new List<Tuple<int, List<Carta>>>();
        private static List<Tuple<int, List<Carta>>> nodosPerdedores = new List<Tuple<int, List<Carta>>>();

        //  ♦  ♥  ♠  ♣
        public static bool Resolver(List<Pico> nivel1, List<Pico> nivel2, List<Pico> nivel3, List<Pico> nivel4, List<Carta> listado,
            out List<Carta> listadoDescubierto, out int puntuacion, out string error)
        {
            puntuacion = 0;
            listadoDescubierto = new List<Carta>();

            try
            {
                var picos = CrearThreePeaks(nivel1, nivel2, nivel3, nivel4, out error);

                if (!RevisarPalo(picos, listado, out error))
                {
                    return false;
                }


                ReiniciarValores();

                Nodo nodo = new Nodo(listado.FirstOrDefault());

                listado.RemoveAt(0);

                ObtenerPosibilidades(nodo, listado, picos);



                if (nodosGanadores.Count == 0)
                {
                    ElejirMejoresPerdedores(1);
                    var item = nodosPerdedores.FirstOrDefault();
                    puntuacion = item.Item1;
                    listadoDescubierto = item.Item2;
                    return false;
                }
                else
                {
                    ElejirMejoresGanadores(1);
                    var item = nodosGanadores.FirstOrDefault();
                    puntuacion = item.Item1;
                    listadoDescubierto = item.Item2;
                    return true;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private static void ReiniciarValores()
        {
            nodosGanadores = new List<Tuple<int, List<Carta>>>();
            nodosPerdedores = new List<Tuple<int, List<Carta>>>();
            ContinuarBuscando = true;
            ConteoIntentosExitosos = 0;
            ConteoIntentosNoExitosos = 0;
        }

        private static bool RevisarPalo(List<Pico> picos, List<Carta> listado, out string error)
        {
            error = string.Empty;
            for (int ip = 0; ip < 4; ip++)
            {
                Palo palo = (Palo)ip;

                if ((picos.Where(p => p.Palo == palo).Count() + listado.Where(p => p.Palo == palo).Count()) != 13)
                {
                    error += $"Hay {picos.Where(p => p.Palo == palo).Count() + listado.Where(p => p.Palo == palo).Count()} cartas de {palo.ToString("g")}. ";

                }

                for (int i = 1; i <= 13; i++)
                {
                    if (picos.Where(p => p.Palo == palo && p.Numero == i).Count() == 0 && listado.Where(p => p.Palo == palo && p.Numero == i).Count() == 0)
                    {
                        error += $"Falta la carta {i} de {palo.ToString("g") }. ";
                    }

                    if (picos.Where(p => p.Palo == palo && p.Numero == i).Count() > 1
                        || listado.Where(p => p.Palo == palo && p.Numero == i).Count() > 1
                        || picos.Where(p => p.Palo == palo && p.Numero == i).Count() >= 1 && listado.Where(p => p.Palo == palo && p.Numero == i).Count() >= 1
                        )
                    {
                        error += $"La carta {i} de {palo.ToString("g") } está repetida. ";
                    }
                }
            }

            return string.IsNullOrEmpty(error);

        }

        private static List<Pico> CrearThreePeaks(List<Pico> nivel1, List<Pico> nivel2, List<Pico> nivel3, List<Pico> nivel4, out string error)
        {
            try
            {
                //nivel 1 = 10 cartas
                //nivel 2 = 9 cartas
                //nivel 3 = 6 cartas
                //nivel 4 = 3 cartas

                List<Pico> cartasEnPicos = new List<Pico>();

                Pico picoBase = null;
                Pico pico1 = new Pico(nivel4[0]) { };//9
                Pico pico2 = new Pico(nivel4[1]);//A
                Pico pico3 = new Pico(nivel4[2]);//Q   

                cartasEnPicos.Add(pico1);
                cartasEnPicos.Add(pico2);
                cartasEnPicos.Add(pico3);

                pico1.InferiorIzquierda = new Pico(nivel3[0]); //10          
                pico1.InferiorDerecha = new Pico(nivel3[1]);//8

                pico2.InferiorIzquierda = new Pico(nivel3[2]);//5
                pico2.InferiorDerecha = new Pico(nivel3[3]);//2

                pico3.InferiorIzquierda = new Pico(nivel3[4]);//3
                pico3.InferiorDerecha = new Pico(nivel3[5]);//6

                cartasEnPicos.Add(pico1.InferiorIzquierda);
                cartasEnPicos.Add(pico1.InferiorDerecha);
                cartasEnPicos.Add(pico2.InferiorIzquierda);
                cartasEnPicos.Add(pico2.InferiorDerecha);
                cartasEnPicos.Add(pico3.InferiorIzquierda);
                cartasEnPicos.Add(pico3.InferiorDerecha);

                pico1.InferiorIzquierda.InferiorIzquierda = new Pico(nivel2[0]);//k
                pico1.InferiorIzquierda.InferiorDerecha = pico1.InferiorDerecha.InferiorIzquierda = new Pico(nivel2[1]);//4            
                pico1.InferiorDerecha.InferiorDerecha = new Pico(nivel2[2]);//3

                pico2.InferiorIzquierda.InferiorIzquierda = new Pico(nivel2[3]);//6
                pico2.InferiorIzquierda.InferiorDerecha = pico2.InferiorDerecha.InferiorIzquierda = new Pico(nivel2[4]);//k         
                pico2.InferiorDerecha.InferiorDerecha = new Pico(nivel2[5]);//j

                pico3.InferiorIzquierda.InferiorIzquierda = new Pico(nivel2[6]);//3
                pico3.InferiorIzquierda.InferiorDerecha = pico3.InferiorDerecha.InferiorIzquierda = new Pico(nivel2[7]);//3        
                pico3.InferiorDerecha.InferiorDerecha = new Pico(nivel2[8]);//q


                cartasEnPicos.Add(pico1.InferiorIzquierda.InferiorIzquierda);
                cartasEnPicos.Add(pico1.InferiorIzquierda.InferiorDerecha);
                cartasEnPicos.Add(pico1.InferiorDerecha.InferiorDerecha);
                cartasEnPicos.Add(pico2.InferiorIzquierda.InferiorIzquierda);
                cartasEnPicos.Add(pico2.InferiorIzquierda.InferiorDerecha);
                cartasEnPicos.Add(pico2.InferiorDerecha.InferiorDerecha);
                cartasEnPicos.Add(pico3.InferiorIzquierda.InferiorIzquierda);
                cartasEnPicos.Add(pico3.InferiorIzquierda.InferiorDerecha);
                cartasEnPicos.Add(pico3.InferiorDerecha.InferiorDerecha);


                pico1.InferiorIzquierda.InferiorIzquierda.InferiorIzquierda = picoBase = new Pico(nivel1[0]);//A
                cartasEnPicos.Add(pico1.InferiorIzquierda.InferiorIzquierda.InferiorIzquierda);
                pico1.InferiorIzquierda.InferiorIzquierda.InferiorDerecha =
                      pico1.InferiorDerecha.InferiorIzquierda.InferiorIzquierda = new Pico(nivel1[1]);//Q
                cartasEnPicos.Add(pico1.InferiorIzquierda.InferiorIzquierda.InferiorDerecha);
                pico1.InferiorIzquierda.InferiorDerecha.InferiorDerecha =
                     pico1.InferiorDerecha.InferiorDerecha.InferiorIzquierda = new Pico(nivel1[2]);//5
                cartasEnPicos.Add(pico1.InferiorIzquierda.InferiorDerecha.InferiorDerecha);
                pico1.InferiorDerecha.InferiorDerecha.InferiorDerecha =
                      pico2.InferiorIzquierda.InferiorIzquierda.InferiorIzquierda = new Pico(nivel1[3]);//4                
                cartasEnPicos.Add(pico1.InferiorDerecha.InferiorDerecha.InferiorDerecha);
                pico2.InferiorIzquierda.InferiorIzquierda.InferiorDerecha =
                      pico2.InferiorDerecha.InferiorIzquierda.InferiorIzquierda = new Pico(nivel1[4]);//9
                cartasEnPicos.Add(pico2.InferiorIzquierda.InferiorIzquierda.InferiorDerecha);
                pico2.InferiorIzquierda.InferiorDerecha.InferiorDerecha =
                     pico2.InferiorDerecha.InferiorDerecha.InferiorIzquierda = new Pico(nivel1[5]);//5
                cartasEnPicos.Add(pico2.InferiorIzquierda.InferiorDerecha.InferiorDerecha);
                pico2.InferiorDerecha.InferiorDerecha.InferiorDerecha =
                   pico3.InferiorIzquierda.InferiorIzquierda.InferiorIzquierda = new Pico(nivel1[6]);//5
                cartasEnPicos.Add(pico2.InferiorDerecha.InferiorDerecha.InferiorDerecha);
                pico3.InferiorIzquierda.InferiorIzquierda.InferiorDerecha =
                   pico3.InferiorDerecha.InferiorIzquierda.InferiorIzquierda = new Pico(nivel1[7]);//4
                cartasEnPicos.Add(pico3.InferiorIzquierda.InferiorIzquierda.InferiorDerecha);
                pico3.InferiorIzquierda.InferiorDerecha.InferiorDerecha =
                     pico3.InferiorDerecha.InferiorDerecha.InferiorIzquierda = new Pico(nivel1[8]);//7
                cartasEnPicos.Add(pico3.InferiorIzquierda.InferiorDerecha.InferiorDerecha);
                pico3.InferiorDerecha.InferiorDerecha.InferiorDerecha = new Pico(nivel1[9]);//10
                cartasEnPicos.Add(pico3.InferiorDerecha.InferiorDerecha.InferiorDerecha);

                pico1.ConocerPadres();
                pico2.ConocerPadres();
                pico3.ConocerPadres();

                cartasEnPicos.Reverse();
                error = string.Empty;
                return cartasEnPicos;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        private static void ObtenerPosibilidades(Nodo nodo, List<Carta> listado, IList<Pico> picos)
        {
            if (!ContinuarBuscando)
                return;

            nodo.Pico.Usada = true;

            List<Pico> posibilidades = picos.Where(p => p.Cubierta == false && p.EsConsecuente(nodo.Pico) && !p.Usada).ToList();

            foreach (var pico in posibilidades)
            {
                nodo.NodosHijos.Add(new Nodo(pico) { NodoPadre = nodo });
            }

            if (posibilidades.Count == 0)
            {
                if (listado.Count() == 0)
                {
                    GuardarNodoPerdedor(nodo);
                }
                else
                {
                    nodo.NodosHijos.Add(new Nodo(listado.FirstOrDefault()) { NodoPadre = nodo });

                    listado.RemoveAt(0);
                }
            }

            foreach (Nodo item in nodo.NodosHijos)
            {
                ObtenerPosibilidades(item, listado, picos);
            }

            if (picos.Where(p => !p.Usada).Count() == 0)
            {
                GuardarNodoGanador(nodo);

                nodo.Pico.Usada = false;

                if (posibilidades.Count == 0 && nodo.NodosHijos.Count > 0)
                {
                    Carta cartaRobada = nodo.NodosHijos.FirstOrDefault().Pico;

                    listado.Insert(0, cartaRobada);
                }

                return;
            }

            nodo.Pico.Usada = false;

            if (posibilidades.Count == 0 && nodo.NodosHijos.Count > 0)
            {
                Carta cartaRobada = nodo.NodosHijos.FirstOrDefault().Pico;

                listado.Insert(0, cartaRobada);
            }

        }

        private static void GuardarNodoGanador(Nodo nodo)
        {
            ConteoIntentosExitosos++;
            nodosGanadores.Add(ObtenerListadoYPuntaje(nodo));

            if (nodosGanadores.Count > 200)
            {
                ElejirMejoresGanadores(10);
            }

            if (MaxIntentosExitosos <= ConteoIntentosExitosos)
            {
                ElejirMejoresGanadores(10);

                ContinuarBuscando = false;
            }

        }

        private static void ElejirMejoresGanadores(int take)
        {
            int nodosMax = nodosGanadores.OrderByDescending(i => i.Item2.Sum(p => p.EsPico ? 1 : 0)).FirstOrDefault().Item2.Sum(p => p.EsPico ? 1 : 0);
            nodosGanadores = nodosGanadores.Where(i => i.Item2.Sum(p => p.EsPico ? 1 : 0) == nodosMax).OrderByDescending(i => i.Item1).Take(take).ToList();
        }

        private static void GuardarNodoPerdedor(Nodo nodo)
        {
            ConteoIntentosNoExitosos++;

            nodosPerdedores.Add(ObtenerListadoYPuntaje(nodo));

            if (nodosPerdedores.Count > 1000)
            {
                ElejirMejoresPerdedores(50);
            }

            if (MaxIntentosNoExitosos <= ConteoIntentosNoExitosos)
            {
                ElejirMejoresPerdedores(50);

                ContinuarBuscando = false;
            }

        }

        private static void ElejirMejoresPerdedores(int take)
        {
            nodosPerdedores = nodosPerdedores.OrderByDescending(i => i.Item1).Take(take).ToList();
        }

        private static Tuple<int, List<Carta>> ObtenerListadoYPuntaje(Nodo nodo)
        {
            var listado = new List<Carta>();
            Nodo nodoPadre = nodo.NodoPadre;
            int cartasSeguidas = 0;
            int puntas = 0;
            int rank = 0;
            while (nodoPadre != null)
            {
                if (nodoPadre.Pico.EsPico)
                {
                    cartasSeguidas++;
                    rank += (200 * cartasSeguidas) - 100;

                    if (nodoPadre.Pico.SuperiorDerecha == null && nodoPadre.Pico.SuperiorIzquierda == null)
                    {
                        puntas++;
                        if (puntas == 1) rank += 500;
                        if (puntas == 2) rank += 1000;
                        if (puntas == 3) rank += 5000;
                    }
                }
                else
                {
                    cartasSeguidas = 0;
                }

                listado.Add(nodoPadre.Pico);
                nodoPadre = nodoPadre.NodoPadre;

            }
            listado.Reverse();

            return new Tuple<int, List<Carta>>(rank, listado);

        }
    }
}
