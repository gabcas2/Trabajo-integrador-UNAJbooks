/*
 * Created by SharpDevelop.
 * User: Familia
 * Date: 28/8/2026
 * Time: 16:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace TP_integrador_2026
{
	class Program
	{
		public static void Main(string[] args)
		{
			List<Libros> ListaLibros = new List<Libros>();
			List<Socios> ListaSocios = new List<Socios>();
			List<Socios_Premium> ListaSociosPremium = new List<Socios_Premium>();
			List<Prestamos> ListaPrestamos = new List<Prestamos>();
			
			
			int opcion;
			do
			{
				Console.WriteLine("                      MENU DE OPCIONES"+
                    "\n +___________________________________________________________+" +  				                  
				    "\n |1- Agregar un libro a la biblioteca                        |" +				    
				    "\n |2- Eliminar libro de la biblioteca                         |" +             
				    "\n |3- Agregar socio						                    |"+
				    "\n |4- Agregar socio premium				                    |"+
				    "\n |5- Eliminar socio	                                        |"+
				    "\n |6- Eliminar socio premium                                  |"+
				    "\n |7- Prestar libro											|"+
				    "\n |8-	Regresar libro											|"+
				    "\n |9- Consultar stock					 						|"+
				    "\n |0 - para salir del programa                                |"+
				    "\n +___________________________________________________________+");
				
				Console.WriteLine("");
				Console.Write("ingresa una opcion: ");
				opcion = int.Parse(Console.ReadLine());
				switch(opcion)
				{
					case 1:
						Console.WriteLine("agregar libro ");
						break;
						
					case 2:
						Console.WriteLine("eliminar libro ");
						break;
						
					case 3:
						Console.WriteLine("agregar socio ");
						break;

					case 4:
						Console.WriteLine("agregar socio premium ");
						break;

					case 5:
						Console.WriteLine("eliminar socio ");
						break;
					
					case 6:
						Console.WriteLine("eliminar socio premium ");
						break;

					case 7:
						Console.WriteLine("prestar libro ");
						break;

					case 8:
						Console.WriteLine("regresar libro ");
						break;

					case 9:
						Console.WriteLine("consultar stock ");
						break;	
						
					case 0:
						Console.WriteLine("saliendo del programa");
						break;

					default:
						Console.WriteLine("elige una opcion valida");
						break;
				}
				
			}while(opcion != 0);
			
			
			Console.ReadKey(true);
		}
		
		//luego voy a mover las clases a otras secciones
		class Socios
		{
			
		}
		
		class Socios_Premium: Socios
		{
			
		}
		
		class Libros
		{
			
		}
		
		class Prestamos
		{
			
		}
		
	}
}