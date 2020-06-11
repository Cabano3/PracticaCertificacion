using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUPeliculas.Transaction
{
	public class PeliculaBLL
	{
		public static Pelicula Get(int? id)
		{
			DBPELICULASEntities db = new DBPELICULASEntities();
			return db.Peliculas.Find(id);
		}

		public static void Create(Pelicula p)
		{
			using (DBPELICULASEntities db = new DBPELICULASEntities())
			{
				using (var transaction = db.Database.BeginTransaction())
				{
					try
					{
						db.Peliculas.Add(p);
						db.SaveChanges();
						transaction.Commit();
					}
					catch (Exception)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
		}

		public static void Update(Pelicula p)
		{
			using (DBPELICULASEntities db = new DBPELICULASEntities())
			{
				using (var transaction = db.Database.BeginTransaction())
				{
					try
					{
						db.Peliculas.Attach(p);
						db.Entry(p).State = System.Data.Entity.EntityState.Modified;
						db.SaveChanges();
						transaction.Commit();
					}
					catch (Exception ex)
					{
						transaction.Rollback();
						throw ex;
					}
				}
			}
		}

		public static void Delete(int? id)
		{
			using (DBPELICULASEntities db = new DBPELICULASEntities())
			{
				using (var transaction = db.Database.BeginTransaction())
				{
					try
					{
						Pelicula pelicula = db.Peliculas.Find(id);
						db.Entry(pelicula).State = System.Data.Entity.EntityState.Deleted;
						db.SaveChanges();
						transaction.Commit();
					}
					catch (Exception ex)
					{
						transaction.Rollback();
						throw ex;
					}
				}
			}
		}

		public static List<Pelicula> List()
		{
			//Instancia del contexto

			/*List<Alumno> alumons = db.Alumnoes.ToList();
            List<Alumno> resultado = new List<Alumno>();
            foreach (Alumno a in alumons) {
                if (a.sexo == "M") {
                    resultado.Add(a);
                }
            }
            return resultado;*/
			//SQL -> SELECT * FROM dbo.Alumno WHERE sexo = 'M'
			//return db.Alumnoes.Where(x => x.sexo == "M").ToList();
			DBPELICULASEntities db = new DBPELICULASEntities();
			return db.Peliculas.ToList();
			//Los metodos del EntityFramework se denomina Linq, 
			//y la evluacion de condiciones lambda
		}
	}
}
