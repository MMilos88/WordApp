using AutoMapper;
using WordApp.BusinessModel.Requests;
using WordApp.BusinessModel.Responses;

namespace WordApp.Mapper
{
    public class Maper : Profile
    {
        public Maper()
        {
            //CreateMap<Text, BusinessModel.Text>();
            //CreateMap<BusinessModel.Text, Text>();

            CreateMap<CalculateWordCountRequest, DataModel.Requests.CalculateWordCountRequest>();
            CreateMap<DataModel.Responses.CalculateWordCountResponse, CalculateWordCountResponse>();
        }
    }
}
