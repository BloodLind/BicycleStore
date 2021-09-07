using BicycleStore.BikesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.BikesDatabase.Context
{
    public class TestBDData
    {
        public static void Initialize(BicycleContext context)
        {
            if(!context.Bicycles.Any())
            {
                context.Bicycles.AddRange(
                    new Bicycle
                    {

                        Tittle = "Bergamont",
                        Model = "ya ebu",
                        Price = 500,
                        Color = "black",
                        Info = "",
                        

                    },
                      new Bicycle
                      {

                          Tittle = "Ashan",
                          Model = "parasha",
                          Price = 50
                          ,
                          Color = "white",
                          Info = "",

                      },
                        new Bicycle
                        {

                            Tittle = "Ardis",
                            Model = "ta pohuy",
                            Price = 300
                            ,
                            Color = "blue",
                            Info = "",

                        },
                          new Bicycle
                          {

                              Tittle = "kyrylchenko prod.",
                              Model = "Лучший",
                              Price = 99999999
                              ,
                              Color = "black",
                              Info = "",

                          },
                            new Bicycle
                            {

                                Tittle = "Ukraina",
                                Model = "zhopagryz",
                                Price = 10
                                ,
                                Color = "white",
                                Info = "",
                            }


                    );
                context.SaveChanges();
            }
        }
    }
}
