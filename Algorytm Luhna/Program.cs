using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Algorytm_Luhna
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");
            const string liczba = "92480";
            const string liczba2 = "51511970227";
            Cyfra cyfra2 = new Cyfra(liczba2);
            Cyfra cyfra = new Cyfra(liczba);
            cyfra.CheckSamokntr();
            cyfra2.CheckSamokntr();
        }
    }

    class Cyfra
    {
        private int _IloscLiczb { get; set; }
        private List<long> _liczba;
        private long _Samokontr { get; set; }

        
        public Cyfra(string liczba)
        {
            if (long.TryParse(liczba, out long result))
            {
                _IloscLiczb = liczba.Length;
                char[] tmp = new char[_IloscLiczb];
                tmp = liczba.ToCharArray();
                
                try
                {

                    _liczba = new List<long>();
                    foreach (var item in tmp)
                    {
                                                                    
                        _liczba.Add((long)Char.GetNumericValue(item));
                        
                    }
                    
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            else
            {
                Console.WriteLine("Nie jest liczbą");
            }
        }
        public void LuhnFindSamo()
        {
            
            long[] temp = _liczba.ToArray();
            try
            {
                for (int i = temp.Length - 1; i >= 0; i--)
                {
                    const int pierw_cyf = 1;
                    if (i % 2 == 0)
                    {
                        temp[i] *= 2;

                        if (temp[i] > 9)
                        {
                            long mod = temp[i] % 10;
                            temp[i] = pierw_cyf + mod;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
           long suma = temp.Sum();
            _Samokontr = suma % 10;
            if (_Samokontr != 0)
                _Samokontr = 10 - _Samokontr;
            _liczba.Add(_Samokontr);
            _IloscLiczb++;
        }// Gdy nie mamy cyfry samokontroli i chcemy ją znaleźć
        bool CheckLuhn()
        {
            long[] temp = _liczba.ToArray();
            Array.Reverse(temp, 0, temp.Length);
            try
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    
                    const int pierw_cyf = 1;
                    if (i % 2 == 1)
                    {
                        
                        temp[i] *= 2;

                        if (temp[i] > 9)
                        {
                            long mod = temp[i] % 10;
                            temp[i] = pierw_cyf + mod;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            long suma = temp.Sum(); 
            if ((suma % 10) == 0)
                return true;
            else
            {
                return false;
            }

        }//Sprawdzamy czy podany ciąg zawiera na końcu cyfrę samokontroli
        public void CheckSamokntr()
        {
            if(CheckLuhn())
                Console.WriteLine($"Poprawna cyfra samokontroli, która wynosi: {_liczba[_IloscLiczb-1]}");
            else
            {
                this.LuhnFindSamo();
                Console.WriteLine($"Cyfra {_liczba[_IloscLiczb-2]} nie jest cyfrą samokontroli. Wynosi ona: {_Samokontr}");
            }
        }
    }
}
