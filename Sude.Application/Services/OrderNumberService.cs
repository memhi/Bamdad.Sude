using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Order;

namespace Sude.Application.Services
{
    public class OrderNumberService : IOrderNumberService
    {
        private IOrderNumberRepository _OrderNumberRepository;

        public OrderNumberService(IOrderNumberRepository orderNumberRepository)
        {
            this._OrderNumberRepository = orderNumberRepository;
        }
        public ResultSet<OrderNumberInfo> GetNewOrderNumberByWorkId(Guid workId, bool isBuy)
        {
            try
            {
                OrderNumberInfo orderNumber = _OrderNumberRepository.GetOrderNumberByWorkId(workId, isBuy);
                if (orderNumber == null)
                {
                    orderNumber = new OrderNumberInfo()
                    {

                        IsBuy = isBuy,
                        WorkId = workId,
                        Year = "",
                        LastNumber = 1
                    };

                }

                else
                    orderNumber.LastNumber += 1;
                return new ResultSet<OrderNumberInfo>()
                {
                    IsSucceed = true,
                    Message = string.Empty,
                    Data = orderNumber
                };

            }

            catch (Exception ex)
            {
                return new ResultSet<OrderNumberInfo>() 
                { 
                    IsSucceed = false,
                    Message = ex.Message,
                    Data=null
                };
            }

        }

        public ResultSet<OrderNumberInfo> GenerateOrderNumberByWorkId(Guid workId, bool isBuy)
        {
            try
            {
                OrderNumberInfo orderNumber = _OrderNumberRepository.GetOrderNumberByWorkId(workId, isBuy);
                if (orderNumber == null)
                {
                    orderNumber = new OrderNumberInfo()
                    {

                        IsBuy = isBuy,
                        WorkId = workId,
                        Year = "",
                        LastNumber = 1
                    };
                    var saveResult = AddOrderNumber(orderNumber);
                  if ( !saveResult.IsSucceed)
                    {

                        return new ResultSet<OrderNumberInfo>()
                        {
                            IsSucceed = false,
                            Message = saveResult.Message,
                            Data = null
                        };
                    }
                       
                }

                else
                {
                    orderNumber.LastNumber += 1;
                    var saveResult = EditOrderNumber(orderNumber);
                    if (!saveResult.IsSucceed)
                    {

                        return new ResultSet<OrderNumberInfo>()
                        {
                            IsSucceed = false,
                            Message = saveResult.Message,
                            Data = null
                        };
                    }
                }
                return new ResultSet<OrderNumberInfo>()
                {
                    IsSucceed = true,
                    Message = string.Empty,
                    Data = orderNumber
                };

            }

            catch (Exception ex)
            {
                return new ResultSet<OrderNumberInfo>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                };
            }

        }





        private ResultSet EditOrderNumber(OrderNumberInfo  orderNumber)
        {


            if (!_OrderNumberRepository.EditOrderNumber(orderNumber))
                return new ResultSet() { IsSucceed = false, Message = "OrderNumber Not Edited" };

            try
            {
                _OrderNumberRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "OrderNumber Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        private ResultSet AddOrderNumber(OrderNumberInfo orderNumber)
        {


            if (!_OrderNumberRepository.AddOrderNumber(orderNumber))
                return new ResultSet() { IsSucceed = false, Message = "OrderNumber Not Edited" };

            try
            {
                _OrderNumberRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "OrderNumber Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }









    }
}
