using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ThreePeaksSolve
{
    class Program
    {
        static string error = string.Empty;

        static void Main(string[] args)
        {
            Console.WriteLine("Three Peaks Solve");
            Console.OutputEncoding = Encoding.UTF8;


            Console.ReadKey();
            // JuegoFacil();
            //JuegoExperto1_1();
            //JuegoExperto1_2();
            //JuegoExperto1_3();
            JuegoMedio1_1();
            //   JuegoMedio1_2();

            GC.Collect();
            Console.ReadKey();
        }

        static void JuegoFacil1()
        {
            var listado = new List<Carta>() {
                new Carta(1, Palo.Corazon), new Carta(13, Palo.Pica), new Carta(11, Palo.Pica), new Carta(12, Palo.Trebol),
                new Carta(7, Palo.Trebol), new Carta(3, Palo.Corazon), new Carta(2, Palo.Diamante), new Carta(2, Palo.Corazon),
                new Carta(5, Palo.Trebol), new Carta(5, Palo.Corazon), new Carta(9, Palo.Trebol), new Carta(6, Palo.Pica),
                new Carta(2, Palo.Trebol), new Carta(11, Palo.Trebol), new Carta(1, Palo.Diamante), new Carta(6, Palo.Corazon),
                new Carta(6, Palo.Trebol), new Carta(7, Palo.Corazon), new Carta(9, Palo.Pica), new Carta(12, Palo.Diamante),
                new Carta(8, Palo.Diamante), new Carta(11, Palo.Diamante), new Carta(10, Palo.Diamante), new Carta(12, Palo.Corazon)
            };
            listado.ForEach(c => { c.EsPico = false; });

            var nivel1 = new List<Pico>() {
                new Pico(2, Palo.Pica), new Pico(9, Palo.Diamante), new Pico(3, Palo.Pica),
                new Pico(4, Palo.Trebol), new Pico(8, Palo.Pica), new Pico(8, Palo.Corazon),
                new Pico(5, Palo.Diamante), new Pico(6, Palo.Diamante), new Pico(7, Palo.Diamante),
                new Pico(8, Palo.Trebol)
            };
            nivel1.ForEach(c => { });

            var nivel2 = new List<Pico>() {
                new Pico(13, Palo.Diamante), new Pico(7, Palo.Pica), new Pico(10, Palo.Trebol), new Pico(3, Palo.Diamante),
                new Pico(4, Palo.Corazon), new Pico(9, Palo.Corazon), new Pico(4, Palo.Diamante), new Pico(3, Palo.Trebol),
                new Pico(12, Palo.Pica)
            };
            nivel2.ForEach(c => { });

            var nivel3 = new List<Pico>() {
                new Pico(10, Palo.Pica), new Pico(13, Palo.Corazon), new Pico(1, Palo.Trebol), new Pico(4, Palo.Pica),
                new Pico(11, Palo.Corazon), new Pico(13, Palo.Trebol)
            };
            nivel3.ForEach(c => { });

            var nivel4 = new List<Pico>() {
                new Pico(5, Palo.Pica), new Pico(10, Palo.Corazon), new Pico(1, Palo.Trebol)
            };
            nivel4.ForEach(c => { });

            Resolver(listado, nivel1, nivel2, nivel3, nivel4);
        }

        private static void Resolver(List<Carta> listado, List<Pico> nivel1, List<Pico> nivel2, List<Pico> nivel3, List<Pico> nivel4)
        {
            List<Carta> listadoDescubierto = new List<Carta>();
            string error = string.Empty;
            ImprimirJuego(listado, nivel1, nivel2, nivel3, nivel4);
            var resultado = ThreePeaks.Resolver(nivel1, nivel2, nivel3, nivel4, listado, out listadoDescubierto, out int puntaje, out error);

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("Error: " + error);
                return;
            }

            Console.WriteLine(!resultado ? $"\nPerdiste\nPuntuación:{puntaje}" : $"\nGanaste\nPuntuación:{puntaje}");

            ImprimirResultado(listadoDescubierto);

            Console.WriteLine("\n\n");

        }

        private static void ImprimirResultado(List<Carta> listado)
        {
            Console.WriteLine("");

            int cartasPorRenglon = 10;

            for (int i = 0; i < (listado.Count); i += cartasPorRenglon)
            {                             
                int index = i;
                string header = string.Empty;
                string carta = string.Empty;
                string cuerpo = string.Empty;
                string footer = string.Empty;
                for (int j = 0; j < cartasPorRenglon; j++)
                {
                    if(index >= listado.Count)                    
                        break;                    

                    if (listado[index].EsPico)
                    {
                        header += "┌────┐ ";
                        carta += $"│{listado[index++]}│►";
                        cuerpo += "│    │ ";
                        footer += "└────┘ ";
                    }
                    else
                    {
                        header += "╔════╗ ";
                        carta += $"║{listado[index++]}║►";
                        cuerpo += "║░░░░║ ";
                        footer += "╚════╝ ";
                    }
                }

                Console.WriteLine(header);
                Console.WriteLine(carta);
                Console.WriteLine(cuerpo);
                Console.WriteLine(footer);
            }
        }

        static void JuegoExperto1_1()
        {
            var listado = new List<Carta>() {
                new Carta(8, Palo.Diamante), new Carta(10, Palo.Trebol),
                new Carta(13, Palo.Trebol), new Carta(11, Palo.Corazon),
                new Carta(12, Palo.Trebol), new Carta(7, Palo.Corazon),
                new Carta(1, Palo.Diamante), new Carta(8, Palo.Corazon),
                new Carta(2, Palo.Trebol), new Carta(10, Palo.Pica),
                new Carta(4, Palo.Diamante), new Carta(5, Palo.Pica),
                new Carta(13, Palo.Pica), new Carta(5, Palo.Diamante),
                new Carta(2, Palo.Corazon), new Carta(4 , Palo.Corazon),
                new Carta(10, Palo.Diamante), new Carta(13, Palo.Diamante),
                new Carta(6, Palo.Diamante), new Carta(9, Palo.Pica),
                new Carta(7, Palo.Trebol), new Carta(11, Palo.Trebol),
                new Carta(11, Palo.Diamante), new Carta(13, Palo.Pica)
            };
            listado.ForEach(c => { c.EsPico = false; });

            var nivel1 = new List<Pico>() {
                new Pico(3, Palo.Trebol), new Pico(1, Palo.Corazon), new Pico(1, Palo.Pica),
                new Pico(9, Palo.Corazon), new Pico(5, Palo.Trebol), new Pico(2, Palo.Pica),
                new Pico(13, Palo.Corazon), new Pico(3, Palo.Corazon), new Pico(8, Palo.Pica),
                new Pico(8, Palo.Trebol)
            };
            nivel1.ForEach(c => { });

            var nivel2 = new List<Pico>() {
                new Pico(12, Palo.Diamante), new Pico(10, Palo.Corazon),
                new Pico(9, Palo.Trebol), new Pico(6, Palo.Corazon),
                new Pico(3, Palo.Diamante), new Pico(5, Palo.Corazon),
                new Pico(7, Palo.Diamante), new Pico(4, Palo.Trebol),
                new Pico(11, Palo.Pica)
            };
            nivel2.ForEach(c => { });


            var nivel3 = new List<Pico>() {
                new Pico(6, Palo.Trebol), new Pico(6, Palo.Pica),
                new Pico(9, Palo.Diamante), new Pico(2, Palo.Diamante),
                new Pico(12, Palo.Corazon), new Pico(1, Palo.Trebol)
            };
            nivel3.ForEach(c => { });

            var nivel4 = new List<Pico>() {
                new Pico(4, Palo.Pica), new Pico(3, Palo.Pica), new Pico(7, Palo.Pica)
            };
            nivel4.ForEach(c => { });

            Resolver(listado, nivel1, nivel2, nivel3, nivel4);
        }

        static void JuegoExperto1_3()
        {
            var listado = new List<Carta>() {
                new Carta(7, Palo.Trebol), new Carta(13, Palo.Pica),
                new Carta(4, Palo.Pica), new Carta(2, Palo.Diamante),
                new Carta(6, Palo.Trebol), new Carta(11, Palo.Diamante),
                new Carta(10, Palo.Corazon), new Carta(1, Palo.Corazon),
                new Carta(11, Palo.Pica), new Carta(2, Palo.Corazon),
                new Carta(13, Palo.Diamante), new Carta(11, Palo.Pica),
                new Carta(8, Palo.Corazon), new Carta(10, Palo.Diamante),
                new Carta(8, Palo.Trebol), new Carta(12, Palo.Pica),
                new Carta(2, Palo.Pica), new Carta(8, Palo.Pica),
                new Carta(9, Palo.Diamante), new Carta(1, Palo.Diamante),
                new Carta(9, Palo.Corazon), new Carta(7, Palo.Diamante),
                new Carta(6, Palo.Diamante), new Carta(7, Palo.Corazon),
            };
            listado.ForEach(c => { c.EsPico = false; });

            var nivel1 = new List<Pico>() {
                new Pico(1, Palo.Pica), new Pico(12, Palo.Diamante),
                new Pico(5, Palo.Diamante), new Pico(4, Palo.Trebol),
                new Pico(9, Palo.Trebol), new Pico(5, Palo.Corazon),
                new Pico(5, Palo.Trebol), new Pico(4, Palo.Diamante),
                new Pico(7, Palo.Pica), new Pico(10, Palo.Pica),
            };
            nivel1.ForEach(c => { });

            var nivel2 = new List<Pico>() {
                new Pico(13, Palo.Corazon), new Pico(4, Palo.Corazon),
                new Pico(3, Palo.Corazon), new Pico(6, Palo.Pica),
                new Pico(13, Palo.Pica), new Pico(11, Palo.Corazon),
                new Pico(3, Palo.Diamante), new Pico(3, Palo.Pica),
                new Pico(12, Palo.Corazon)
            };
            nivel2.ForEach(c => { });


            var nivel3 = new List<Pico>() {
                new Pico(10, Palo.Trebol), new Pico(8, Palo.Diamante),
                new Pico(5, Palo.Pica), new Pico(2, Palo.Trebol),
                new Pico(3, Palo.Trebol), new Pico(6, Palo.Corazon),
            };
            nivel3.ForEach(c => { });

            var nivel4 = new List<Pico>() {
                new Pico(9, Palo.Pica), new Pico(1, Palo.Trebol),
                new Pico(12, Palo.Trebol)
            };
            nivel4.ForEach(c => { });

            Resolver(listado, nivel1, nivel2, nivel3, nivel4);
        }

        static void JuegoExperto1_2()
        {
            var listado = new List<Carta>() {
                new Carta(5, Palo.Trebol), new Carta(4, Palo.Trebol),
                new Carta(7, Palo.Diamante), new Carta(3, Palo.Trebol),
                new Carta(6, Palo.Corazon), new Carta(6, Palo.Pica),
                new Carta(7, Palo.Pica), new Carta(4, Palo.Diamante),
                new Carta(11, Palo.Pica), new Carta(11, Palo.Trebol),
                new Carta(3, Palo.Pica), new Carta(4, Palo.Pica),
                new Carta(13, Palo.Trebol), new Carta(8, Palo.Diamante),
                new Carta(5, Palo.Corazon), new Carta(1, Palo.Pica),
                new Carta(12, Palo.Trebol), new Carta(2, Palo.Trebol),
                new Carta(2, Palo.Corazon), new Carta(8, Palo.Corazon),
                new Carta(12, Palo.Corazon), new Carta(13, Palo.Corazon),
                new Carta(10, Palo.Diamante), new Carta(9, Palo.Diamante),
            };
            listado.ForEach(c => { c.EsPico = false; });

            var nivel1 = new List<Pico>() {
                new Pico(6, Palo.Diamante), new Pico(5, Palo.Pica),
                new Pico(12, Palo.Pica), new Pico(2, Palo.Pica),
                new Pico(1, Palo.Diamante), new Pico(7, Palo.Trebol),
                new Pico(4, Palo.Corazon), new Pico(6, Palo.Trebol),
                new Pico(10, Palo.Trebol), new Pico(10, Palo.Corazon),
            };
            nivel1.ForEach(c => { });

            var nivel2 = new List<Pico>() {
                new Pico(13, Palo.Pica), new Pico(9, Palo.Pica),
                new Pico(10, Palo.Pica), new Pico(8, Palo.Pica),
                new Pico(11, Palo.Corazon), new Pico(5, Palo.Diamante),
                new Pico(9, Palo.Corazon), new Pico(1, Palo.Trebol),
                new Pico(11, Palo.Diamante)
            };
            nivel2.ForEach(c => { });


            var nivel3 = new List<Pico>() {
                new Pico(8, Palo.Trebol), new Pico(13, Palo.Pica),
                new Pico(1, Palo.Corazon), new Pico(12, Palo.Diamante),
                new Pico(3, Palo.Diamante), new Pico(2, Palo.Diamante),
            };
            nivel3.ForEach(c => { });

            var nivel4 = new List<Pico>() {
                new Pico(3, Palo.Corazon), new Pico(7, Palo.Corazon),
                new Pico(9, Palo.Trebol)
            };
            nivel4.ForEach(c => { });

            Resolver(listado, nivel1, nivel2, nivel3, nivel4);
        }

        static void JuegoMedio1_1()
        {
            var listado = new List<Carta>() {
                new Carta(8, Palo.Pica),
                new Carta(1, Palo.Corazon),
                new Carta(9, Palo.Corazon),
                new Carta(13, Palo.Trebol),
                new Carta(6, Palo.Trebol),
                new Carta(1, Palo.Diamante),
                new Carta(10, Palo.Diamante),
                new Carta(7, Palo.Diamante),
                new Carta(13, Palo.Pica),
                new Carta(8, Palo.Trebol),
                new Carta(12, Palo.Pica),
                new Carta(3, Palo.Pica),
                new Carta(4, Palo.Pica),
                new Carta(6, Palo.Corazon),
                new Carta(3, Palo.Diamante),
                new Carta(7, Palo.Trebol),
                new Carta(2, Palo.Trebol),
                new Carta(2, Palo.Corazon),
                new Carta(9, Palo.Diamante),
                new Carta(7, Palo.Pica),
                new Carta(3, Palo.Trebol),
                new Carta(8, Palo.Diamante),
                new Carta(7, Palo.Corazon),
                new Carta(3, Palo.Corazon),
            };
            listado.ForEach(c => { c.EsPico = false; });

            var nivel1 = new List<Pico>() {
                new Pico(4, Palo.Diamante),
                new Pico(10, Palo.Pica),
                new Pico(2, Palo.Pica),
                new Pico(10, Palo.Corazon),
                new Pico(11, Palo.Pica),
                new Pico(12, Palo.Diamante),
                new Pico(11, Palo.Trebol),
                new Pico(9, Palo.Trebol),
                new Pico(2, Palo.Diamante),
                new Pico(13, Palo.Diamante),
            };
            nivel1.ForEach(c => { });

            var nivel2 = new List<Pico>() {
                new Pico(6, Palo.Diamante),
                new Pico(11, Palo.Corazon),
                new Pico(10, Palo.Trebol),

                new Pico(5, Palo.Pica),
                new Pico(5, Palo.Trebol),
                new Pico(5, Palo.Corazon),

                new Pico(5, Palo.Diamante),
                new Pico(1, Palo.Trebol),
                new Pico(1, Palo.Pica)
            };
            nivel2.ForEach(c => { });


            var nivel3 = new List<Pico>() {
                new Pico(4, Palo.Trebol),
                new Pico(12, Palo.Corazon),

                new Pico(13, Palo.Corazon),
                new Pico(9, Palo.Pica),

                new Pico(12, Palo.Trebol),
                new Pico(4, Palo.Corazon),
            };
            nivel3.ForEach(c => { });

            var nivel4 = new List<Pico>() {
                new Pico(11, Palo.Diamante),
                new Pico(8, Palo.Corazon),
                new Pico(6, Palo.Pica)
            };
            nivel4.ForEach(c => { });

            Resolver(listado, nivel1, nivel2, nivel3, nivel4);
        }

        static void JuegoMedio1_2()
        {
            var listado = new List<Carta>() {
                new Carta(11, Palo.Trebol),
                new Carta(12, Palo.Diamante),
                new Carta(1, Palo.Corazon),
                new Carta(13, Palo.Diamante),
                new Carta(2, Palo.Trebol),
                new Carta(10, Palo.Diamante),
                new Carta(3, Palo.Pica),
                new Carta(4, Palo.Trebol),
                new Carta(1, Palo.Trebol),
                new Carta(9, Palo.Corazon),
                new Carta(7, Palo.Trebol),
                new Carta(3, Palo.Diamante),
                new Carta(4, Palo.Corazon),
                new Carta(5, Palo.Corazon),
                new Carta(2, Palo.Pica),
                new Carta(13, Palo.Trebol),
                new Carta(3, Palo.Corazon),
                new Carta(8, Palo.Diamante),
                new Carta(3, Palo.Trebol),
                new Carta(5, Palo.Pica),
                new Carta(9, Palo.Diamante),
                new Carta(4, Palo.Pica),
                new Carta(2, Palo.Corazon),
                new Carta(9, Palo.Trebol),

            };
            listado.ForEach(c => { c.EsPico = false; });

            var nivel1 = new List<Pico>() {
                new Pico(7, Palo.Diamante),
                new Pico(10, Palo.Pica),
                new Pico(10, Palo.Trebol),

                new Pico(13, Palo.Corazon),
                new Pico(1, Palo.Pica),
                new Pico(11, Palo.Diamante),
                new Pico(2, Palo.Diamante),

                new Pico(12, Palo.Pica),
                new Pico(11, Palo.Pica),
                new Pico(8, Palo.Pica),
            };
            nivel1.ForEach(c => { });

            var nivel2 = new List<Pico>() {
                new Pico(8, Palo.Trebol),
                new Pico(6, Palo.Pica),
                new Pico(7, Palo.Pica),

                new Pico(8, Palo.Corazon),
                new Pico(7, Palo.Corazon),
                new Pico(6, Palo.Diamante),

                new Pico(6, Palo.Corazon),
                new Pico(13, Palo.Pica),
                new Pico(12, Palo.Trebol)
            };
            nivel2.ForEach(c => { });


            var nivel3 = new List<Pico>() {
                new Pico(4, Palo.Diamante),
                new Pico(1, Palo.Diamante),

                new Pico(5, Palo.Trebol),
                new Pico(5, Palo.Diamante),

                new Pico(6, Palo.Trebol),
                new Pico(9, Palo.Pica),
            };
            nivel3.ForEach(c => { });

            var nivel4 = new List<Pico>() {
                new Pico(10, Palo.Corazon),
                new Pico(12, Palo.Corazon),
                new Pico(11, Palo.   Corazon)
            };
            nivel4.ForEach(c => { });

            Resolver(listado, nivel1, nivel2, nivel3, nivel4);
        }

        static void JuegoVacio()
        {
            var listado = new List<Carta>() {
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
                new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),
  new Carta(0, Palo.Indiferente), new Carta(0, Palo.Indiferente),

            };
            listado.ForEach(c => { c.EsPico = false; });

            var nivel1 = new List<Pico>() {
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
            };
            nivel1.ForEach(c => { });

            var nivel2 = new List<Pico>() {
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente)
            };
            nivel2.ForEach(c => { });


            var nivel3 = new List<Pico>() {
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
            };
            nivel3.ForEach(c => { });

            var nivel4 = new List<Pico>() {
                new Pico(0, Palo.Indiferente), new Pico(0, Palo.Indiferente),
                new Pico(0, Palo.Indiferente)
            };
            nivel4.ForEach(c => { });

            Resolver(listado, nivel1, nivel2, nivel3, nivel4);
        }

        static void ImprimirJuego(List<Carta> listado, List<Pico> nivel1, List<Pico> nivel2, List<Pico> nivel3, List<Pico> nivel4)
        {
            Console.WriteLine("\n\n");
            //   ─   └─┘
            // --espacios--[K<3]--espacios--[K<3]--espacios--[K<3]--espacios--  
            Console.WriteLine("            ┌────┐                  ┌────┐                  ┌────┐");
            Console.WriteLine("            " + $"│{nivel4[0]}│" + $"                  " + $"│{nivel4[1]}│" + $"                  " + $"│{nivel4[2]}│");

         //   Console.WriteLine("            │    │                  │    │                  │    │ ");
            Console.WriteLine("        ┌───┴┐  ┌┴───┐          ┌───┴┐  ┌┴───┐          ┌───┴┐  ┌┴───┐");
            Console.WriteLine($"        " + $"│{nivel3[0]}├──" + $"┤{nivel3[1]}│  " + $"        " +
                                            $"│{nivel3[2]}├──" + $"┤{nivel3[3]}│  " + $"        " +
                                            $"│{nivel3[4]}├──" + $"┤{nivel3[5]}│");
        //    Console.WriteLine("        │    │  │    │          │    │  │    │          │    │  │    │");
            Console.WriteLine("    ┌───┴┐  ┌┴──┴┐  ┌┴───┐  ┌───┴┐  ┌┴──┴┐  ┌┴───┐  ┌───┴┐  ┌┴──┴┐  ┌┴───┐");
            Console.WriteLine($"    " +
                                $"│{nivel2[0]}├──" + $"┤{nivel2[1]}├──" + $"┤{nivel2[2]}│  " + $"" +
                                $"│{nivel2[3]}├──" + $"┤{nivel2[4]}├──" + $"┤{nivel2[5]}│  " + $"" +
                                $"│{nivel2[6]}├──" + $"┤{nivel2[7]}├──" + $"┤{nivel2[8]}│ ");


        //    Console.WriteLine("    │    │  │    │  │    │  │    │  │    │  │    │  │    │  │    │  │    │");
            Console.WriteLine("┌───┴┐  ┌┴──┴┐  ┌┴──┴┐  ┌┴──┴┐  ┌┴──┴┐  ┌┴──┴┐  ┌┴──┴┐  ┌┴──┴┐  ┌┴──┴┐  ┌┴───┐");
            int index = 0;
            Console.WriteLine($"" +
                                $"│{nivel1[index++]}├──" + $"│{nivel1[index++]}├──" + $"┤{nivel1[index++]}├──" +
                                $"┤{nivel1[index++]}├──" + $"┤{nivel1[index++]}├──" + $"┤{nivel1[index++]}├──" +
                                $"┤{nivel1[index++]}├──" + $"┤{nivel1[index++]}├──" + $"┤{nivel1[index++]}├──" +
                                $"┤{nivel1[index++]}│"
                );
           // Console.WriteLine("│    │  │    │  │    │  │    │  │    │  │    │  │    │  │    │  │    │  │    │");
            Console.WriteLine("│    │  │    │  │    │  │    │  │    │  │    │  │    │  │    │  │    │  │    │");
            Console.WriteLine("└────┘  └────┘  └────┘  └────┘  └────┘  └────┘  └────┘  └────┘  └────┘  └────┘");





            Console.WriteLine("  ┌────┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┐");
            string cartas = "  │  ";

            foreach (var item in listado)
            {
                if (item.Numero == 10)
                    cartas += "0" + item.ToString().Substring(3, 1) + "│";
                else
                    cartas += item.ToString().Trim() + "│";
            }

            Console.WriteLine(cartas);
          //  Console.WriteLine("  │    │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │");
            Console.WriteLine("  │░░░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│░░│");
            Console.WriteLine("  └────┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┴──┘");

        }

    }
}
