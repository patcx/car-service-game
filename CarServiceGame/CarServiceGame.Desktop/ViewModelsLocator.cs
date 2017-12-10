using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Desktop.Helpers;
using CarServiceGame.Desktop.ViewModels;

namespace CarServiceGame.Desktop
{
    public class ViewModelsLocator
    {
        public MainViewModel MainViewModel { get; set; } = NinjectBinder.Get<MainViewModel>();
        public OrdersCollectionViewModel OrdersCollection { get; set; } = NinjectBinder.Get<OrdersCollectionViewModel>();
        public AvailableWorkersCollectionViewModel AvailableWorkersCollection { get; set; } = NinjectBinder.Get<AvailableWorkersCollectionViewModel>();
        public HistoryOrdersCollectionViewModel HistoryOrdersCollection { get; set; } = NinjectBinder.Get<HistoryOrdersCollectionViewModel>();
        public RankingViewModel Ranking { get; set; } = NinjectBinder.Get<RankingViewModel>();
    }
}
