using AutoMapper;
using Database;
using GigaKino.ObjectsDTO;
using GigaKino.Models;
using GigaKino.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace GigaKino.Services
{
    public class RepertuarService : IRepertuarService
    {
        private readonly KinoContext _context;
        private readonly IMapper _mapper;

        public RepertuarService(KinoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RepertuarItemDTO>?> GetRepertuarAsync(uint kinoId, string? tytulFilmu = null, string? gatunek = null, string? technologia = null, string? wersjaJezykowa = null)
        {
            string query =
            "SELECT Film.idFilm,Tytul,Ogr_wiekowe,Gatunek,PosterPath,Dlugosc,Technologia,Wersja_Jezykowa,idSeans,termin FROM Seans "+
            "JOIN Film ON Seans.idFilm = Film.idFilm "+
            "JOIN Sala ON Seans.idSala = Sala.idSala "+
            $"WHERE Sala.idKino = @kino_id "+
            $"AND (@tytul_filmu IS NULL OR Film.Tytul = @tytul_filmu) "+
            $"AND (@gatunek IS NULL OR Film.Gatunek LIKE @gatunek) "+
            $"AND (@technologia IS NULL OR Seans.Technologia = @technologia) "+
            $"AND (@wersja_jezykowa IS NULL OR Seans.Wersja_jezykowa = @wersja_jezykowa )";

            var connection = (MySqlConnection)_context.Database.GetDbConnection();
            MySqlCommand cmd = new(query, connection);
            List<RepertuarItemDTO> results = [];

            try
            {
                await connection.OpenAsync();

                cmd.Parameters.Add(new MySqlParameter("@kino_id", MySqlDbType.UInt32) { Value = kinoId });
                cmd.Parameters.Add(new MySqlParameter("@tytul_filmu", MySqlDbType.VarChar, 100) { Value = string.IsNullOrEmpty(tytulFilmu) ? (object)DBNull.Value : tytulFilmu });
                cmd.Parameters.Add(new MySqlParameter("@gatunek", MySqlDbType.VarChar, 50) { Value = string.IsNullOrEmpty(gatunek) ? (object)DBNull.Value : "%"+gatunek+"%" });
                cmd.Parameters.Add(new MySqlParameter("@technologia", MySqlDbType.VarChar, 30) { Value = string.IsNullOrEmpty(technologia) ? (object)DBNull.Value : technologia });
                cmd.Parameters.Add(new MySqlParameter("@wersja_jezykowa", MySqlDbType.VarChar, 20) { Value = string.IsNullOrEmpty(wersjaJezykowa) ? (object)DBNull.Value : wersjaJezykowa });

                MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    int filmIndex = results.FindIndex(item => item.IdFilm == reader.GetUInt32("idFilm"));
                    if (filmIndex < 0)
                    {
                        results.Add(new RepertuarItemDTO(reader.GetUInt32("idFilm"),reader.GetString("Tytul"),reader.GetInt32("Ogr_wiekowe"),reader.GetString("PosterPath"),reader.GetString("Gatunek"),reader.GetInt32("Dlugosc"),reader.GetString("Technologia"),reader.GetString("Wersja_Jezykowa"),reader.GetUInt32("idSeans"),reader.GetDateTime("Termin")));
                    }
                    else
                    {
                        int techIndex = results[filmIndex].ListaWersjiTechnologicznych.FindIndex(item => item.Wersja == reader.GetString("Technologia"));
                        if (techIndex < 0)
                        {
                            results[filmIndex].ListaWersjiTechnologicznych.Add(new WersjaTechnologiczna (reader.GetString("Technologia"),reader.GetString("Wersja_Jezykowa"),reader.GetUInt32("idSeans"),reader.GetDateTime("Termin")));
                        }
                        else
                        {
                            int jezykIndex = results[filmIndex].ListaWersjiTechnologicznych[techIndex].ListaWersjiJezykowych.FindIndex(item => item.Wersja == reader.GetString("Wersja_Jezykowa"));
                            if (jezykIndex < 0)
                            {
                                results[filmIndex].ListaWersjiTechnologicznych[techIndex].ListaWersjiJezykowych.Add(new WersjaJezykowa(reader.GetString("Wersja_Jezykowa"), reader.GetUInt32("idSeans"),reader.GetDateTime("Termin")));
                            }
                            else
                            {
                                results[filmIndex].ListaWersjiTechnologicznych[techIndex].ListaWersjiJezykowych[jezykIndex].ListaSeansow.Add(new Repertuar_Seans(reader.GetUInt32("idSeans"),reader.GetDateTime("Termin")));
                            }
                        }
                    }
                }
                // foreach(RepertuarItemDTO film in results)
                // {
                //     Console.WriteLine("Tytuł: "+ film.Tytul);
                //     foreach(WersjaTechnologiczna tech in film.ListaWersjiTechnologicznych)
                //     {
                //         Console.WriteLine("\tTechnologia: "+ tech.Wersja);
                //         foreach(WersjaJezykowa jezyk in tech.ListaWersjiJezykowych)
                //         {
                //             Console.WriteLine("\t\t"+jezyk.Wersja);
                //             foreach(Repertuar_Seans seans in jezyk.ListaSeansow)
                //             {
                //                 Console.WriteLine("\t\t\t"+seans.Termin);
                //             }
                //         }
                //     }
                // }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetRepertuarAsync failed:"+ ex);
            }
            finally
            {
                cmd?.Dispose();
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            return results;
        }
    }
}