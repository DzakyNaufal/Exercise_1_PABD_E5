using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password : ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan : ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = LAPTOP-69KQVMS1\\DZAKY_NAUFAL; " +
                                    "initial catalog = {0}; " + "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Ubah Data");
                                        Console.WriteLine("4. Cari Data");
                                        Console.WriteLine("5. Hapus Data");
                                        Console.WriteLine("6. Keluar");
                                        Console.WriteLine("\nEnter your choice (1-6): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA KOPERASI\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA KOPERASI\n");
                                                    Console.WriteLine("Masukkan ID_Pelanggan : ");
                                                    string ID_Pelanggan = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Jalan : ");
                                                    string Jalan = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Kota : ");
                                                    string Kota = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Kode Pos");
                                                    string Kode_Pos = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(ID_Pelanggan, Jalan, Kota, Kode_Pos, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " +
                                                            "akses untuk tambahan data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;

                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered");
                                    }

                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From Pelanggan", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string ID_Pelanggan, string Jalan, string Kota, string Kode_Pos,
            SqlConnection con)
        {
            string str = "";
            str = "insert into dbo.Pelanggan (ID_Pelanggan, Jalan, Kota, Kode_Pos)"
                + " values (@ID_Pelanggan, @Jalan, @Kota, @Kode_Pos)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("ID_Pelanggan", ID_Pelanggan));
            cmd.Parameters.Add(new SqlParameter("Jalan", Jalan));
            cmd.Parameters.Add(new SqlParameter("Kota", Kota));
            cmd.Parameters.Add(new SqlParameter("Kode_Pos", Kode_Pos));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}
    