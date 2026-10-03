using System;using System.Globalization;using System.Threading;
class Program {static void Main(){Test7();Test8();Console.WriteLine("PASS: both Phone targets, missing fields/null query, case-insensitive title/price/description and clearing query");}
static void Test7(){var vm=new TransportPricesWP.ViewModel.MainViewModel();var a=new TransportPricesWP.ViewModel.PriceItem{Title="METRO",Price="42",Description="Express"};vm.Items.Add(new TransportPricesWP.ViewModel.PriceItem());vm.Items.Add(a);Thread.CurrentThread.CurrentCulture=new CultureInfo("en-US");
foreach(var query in new[]{"metro","42","EXPRESS"}){vm.SearchQuery=query;if(vm.ResultItems.Count!=1||vm.ResultItems[0]!=a)throw new Exception("Search failed: "+query);}
vm.SearchQuery="missing";if(vm.ResultItems.Count!=0)throw new Exception("Stale results");vm.SearchQuery=null;if(vm.ResultItems.Count!=2)throw new Exception("Clearing null query failed");vm.SearchQuery="";if(vm.ResultItems.Count!=2)throw new Exception("Empty query failed");}
static void Test8(){var vm=new TransportPricesWP8.ViewModel.MainViewModel();var a=new TransportPricesWP8.ViewModel.PriceItem{Title="METRO",Price="42",Description="Express"};vm.Items.Add(new TransportPricesWP8.ViewModel.PriceItem());vm.Items.Add(a);Thread.CurrentThread.CurrentCulture=new CultureInfo("en-US");
foreach(var query in new[]{"metro","42","EXPRESS"}){vm.SearchQuery=query;if(vm.ResultItems.Count!=1||vm.ResultItems[0]!=a)throw new Exception("Search failed: "+query);}
vm.SearchQuery="missing";if(vm.ResultItems.Count!=0)throw new Exception("Stale results");vm.SearchQuery=null;if(vm.ResultItems.Count!=2)throw new Exception("Clearing null query failed");vm.SearchQuery="";if(vm.ResultItems.Count!=2)throw new Exception("Empty query failed");}
}
