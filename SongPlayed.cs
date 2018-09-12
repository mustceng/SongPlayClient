using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace PI_WORKS
{
    class Program
    {
        static void Main(string[] args)
        {
            {
			// input file
                using (var reader = new StreamReader(@"D:\exhibitA-input.csv")) {

                    List<string> PLAY_ID = new List<string>();
                    List<string> SONG_ID = new List<string>();
                    List<string> CLIENT_ID = new List<string>();
                    List<string> PLAY_TS = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split('\t');
                        if (values[3].Contains("10/08/2016"))
                        {
                            PLAY_ID.Add(values[0]);
                            SONG_ID.Add(values[1]);
                            CLIENT_ID.Add(values[2]);
                            PLAY_TS.Add(values[3]);
                        }


                    }


                    HashSet<string> IDs = new HashSet<string>(CLIENT_ID);//removing duplicate
                    string[] arr = IDs.ToArray();
                    int[] dis = new int[IDs.Count()];
                    var alreadySeen = new HashSet<string>();
                    for (int i = 1; i < PLAY_ID.Count(); i++)
                    {
                        for (int j = 1; j < arr.Count(); j++)
                        {
                            if (CLIENT_ID[i] == arr[j])
                            {
                                if (alreadySeen.Add(SONG_ID[i]))
                                {
                                    dis[j]++;
                                }
                            }
                        }
                    }

                    
                    int count = 0;
                    HashSet<int> occ = new HashSet<int>(dis);
                    int[] arr2 = occ.ToArray();
                    List<int> list = new List<int>();
                    for(int i = 0; i < arr2.Count(); i++)
                    {
                        for(int j=0; j < dis.Count(); j++)
                        {
                            if(arr2[i] == dis[j])
                            {
                                count++;
                            }
                        }
                        list.Add(count);
                        count = 0;
                    }
                    int max_distinct = 0;
                    int count346_index = -1;

                    string filePath = @"D:\output.csv";//output file 
                    string delimiter = "\t";
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("DISTINCT_PLAY_COUNT" + delimiter + "CLIENT_COUNT");

                    


                    for (int i = 1; i < arr2.Count(); i++)
                    {
                        if(arr2[i] == 346)
                        {
                            count346_index=i;
                        }
                        if (arr2[i] > max_distinct)
                        {
                            max_distinct = arr2[i];
                        }
                        Console.WriteLine("DISTINCT_PLAY_COUNT: " + arr2[i] + " CLIENT_COUNT: " + list[i]);
                        sb.AppendLine(arr2[i].ToString() + delimiter + list[i].ToString());
                    }

                    File.WriteAllText(filePath, sb.ToString());

                    if (count346_index == -1)
                    {
                        Console.WriteLine("There are 0 sers played 346 distinct songs\n");
                    }
                    else
                    {
                        Console.WriteLine("There are" + arr2[count346_index] + "sers played 346 distinct songs\n");
                    }

                    Console.WriteLine("Maximum number of distinct songs played: " + max_distinct);

                }
            }
        }

    }
}
