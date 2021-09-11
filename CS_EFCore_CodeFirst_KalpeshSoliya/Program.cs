using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using CS_EFCore_CodeFirst_KalpeshSoliya.Models;
using CS_EFCore_CodeFirst_KalpeshSoliya.Services;

namespace CS_EFCore_CodeFirst_KalpeshSoliya
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                bool _Exit = false;
                ScikeyLabDbContext ctx = new ScikeyLabDbContext();
                do
                {
                    Console.WriteLine("1.Category");
                    Console.WriteLine("2.Product");
                    Console.WriteLine("3.Vender");
                    Console.WriteLine("4.VenderProducts");
                    Console.WriteLine("5.Exit");
                    Console.WriteLine("Please Select One Optione from above:");
                    string i = Console.ReadLine();
                    
                    switch (i)
                    {
                        case "1":
                            await Operation("Category", ctx);
                            _Exit = false;
                            break;
                        case "2":
                            await Operation("Product", ctx);
                            _Exit = false;
                            break;
                        case "3":
                            await Operation("Vender", ctx);
                            _Exit = false;
                            break;
                        case "4":
                            await Operation("VenderProducts", ctx);
                            _Exit = false;
                            break;
                        case "5":
                            _Exit = true;
                            Console.WriteLine("Bye Bye...");
                            break;
                        default:
                            Console.WriteLine("Please entry valid number...");
                            _Exit = false;
                            break;
                    }
                } while (!_Exit);

                static async Task Operation(string _Table, ScikeyLabDbContext _ctx)
                {
                    bool _Exit = false;
                    
                        CategoryService CServ = new(_ctx);
                        ProductService PServ = new(_ctx);
                        VenderService VServ = new(_ctx);
                        VendorProductService VPServ = new(_ctx);
                    
                    do
                    {
                        Console.WriteLine("1.Add");
                        Console.WriteLine("2.Change");
                        Console.WriteLine("3.Find");
                        Console.WriteLine("4.Delete");
                        Console.WriteLine("5.Exit");
                        Console.WriteLine("Please Select One Optione from above:");
                        string c = Console.ReadLine();
                        switch (c)
                        {
                            case "1":
                                if (_Table == "Category")
                                {
                                    Category cat = new Category();

                                    Console.WriteLine("CategoryId: ");
                                    cat.CategoryId = Console.ReadLine();
                                    Console.WriteLine("CategoryName: ");
                                    cat.CategoryName = Console.ReadLine();

                                    var newcat = await CServ.CreateAsync(cat);
                                    Console.WriteLine($"Result After Add= {JsonSerializer.Serialize(newcat)}");
                                }
                                if (_Table == "Product")
                                {
                                    Products pro = new Products();

                                    Console.WriteLine("ProductId: ");
                                    pro.ProductId = Console.ReadLine();
                                    Console.WriteLine("ProductName: ");
                                    pro.ProductName = Console.ReadLine();
                                    Console.WriteLine("CategoryId: ");
                                    pro.CategoryRowId = Convert.ToInt16(Console.ReadLine());

                                    var newpro = await PServ.CreateAsync(pro);
                                    Console.WriteLine($"Result After Add= {JsonSerializer.Serialize(newpro)}");
                                }
                                if (_Table == "Vender")
                                {
                                    Vender vdr = new Vender();

                                    Console.WriteLine("VenderId: ");
                                    vdr.VenderId = Console.ReadLine();
                                    Console.WriteLine("VenderName: ");
                                    vdr.VenderName = Console.ReadLine();

                                    var newvdr = await VServ.CreateAsync(vdr);
                                    Console.WriteLine($"Result After Add= {JsonSerializer.Serialize(vdr)}");
                                }
                                if (_Table == "VenderProducts")
                                {
                                    Vender clsVdr = new Vender();
                                    //List<Products> clsProList = new List<Products>();
                                    
                                    Console.WriteLine("VenderId: ");
                                    string VID = Console.ReadLine();
                                    clsVdr = await VServ.GetbyVIDAsync(VID);

                                    Console.WriteLine("ProductId(With comma sperator): ");
                                    string PID = Console.ReadLine();
                                    var clsProList = await PServ.GetbyPIdAsync(PID);

                                    foreach (Products clsPro in clsProList)
                                    {
                                        VendorProduct vdrp = new VendorProduct();
                                        vdrp.VenderRowId = clsVdr.VenderRowId;
                                        vdrp.ProductRowId = clsPro.ProductRowId;

                                        var newvdrp = await VPServ.CreateAsync(vdrp);
                                        Console.WriteLine($"Result After Add= {JsonSerializer.Serialize(vdrp)}");
                                    }

                                }
                                _Exit = false;
                                break;
                            case "2":
                                if (_Table == "Category")
                                {

                                }
                                if (_Table == "Product")
                                {

                                }
                                if (_Table == "Vender")
                                {

                                }
                                if (_Table == "VenderProducts")
                                {

                                }
                                _Exit = false;
                                break;
                            case "3":
                                if (_Table == "Category")
                                {

                                }
                                if (_Table == "Product")
                                {

                                }
                                if (_Table == "Vender")
                                {

                                }
                                if (_Table == "VenderProducts")
                                {

                                }
                                _Exit = false;
                                break;
                            case "4":
                                if (_Table == "Category")
                                {

                                }
                                if (_Table == "Product")
                                {

                                }
                                if (_Table == "Vender")
                                {

                                }
                                if (_Table == "VenderProducts")
                                {

                                }
                                _Exit = false;
                                break;
                            case "5":
                                Console.WriteLine("Bye Bye From " + _Table);
                                _Exit = true;
                                break;
                            default:
                                Console.WriteLine("Please entry valid number...");
                                _Exit = false;
                                break;
                        }
                    } while (!_Exit);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Occured {ex.Message}");
            }
        }
    }
}
