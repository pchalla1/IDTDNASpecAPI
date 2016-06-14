using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataAccess.UnitOfWork;
using DataAccess;
using System.Linq.Expressions;
using System;
using Helper;
using System.Data;

namespace BusinessServices
{
    /// <summary>
    /// Offers services for product specific CRUD operations
    /// </summary>
    public class SpecDeliveryServices : ISpecDeliverySevices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public SpecDeliveryServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets spec deliveries based on filter in query
        /// </summary>
        /// <param name="query">Custom query string</param>
        /// <returns></returns>
        public IEnumerable<Spec_DeliveryEntity> GetSpecDeliveriesByFilter(string query)
        {
            var data = _unitOfWork.SpecDeliveryRepository.GetWithRawSql(query);
            return null;
        }


        /// <summary>
        /// Gets spec deliveries based on spec inputs
        /// </summary>
        /// <param name="specInputs">Enduser input</param>
        /// <returns></returns>
        public DataTable GetSpecDeliveriesByFilter(List<Spec_Input> specInputs)
        {
            //Expression<Func<SPEC_DELIVERY, bool>> customLambda 
            //    = ExpressionCreater<SPEC_DELIVERY>.GetWhereLambda(specInput.InputIds.FirstOrDefault(), specInput.FieldName, specInput.Condition);
            //var specDeliveries = _unitOfWork.SpecDeliveryRepository.GetDataByFilter(customLambda).ToList();
            //if (specDeliveries.Any())
            //{
            //    Mapper.CreateMap<SPEC_DELIVERY, Spec_DeliveryEntity>();
            //    var model = Mapper.Map<List<SPEC_DELIVERY>, List<Spec_DeliveryEntity>>(specDeliveries);
            //    return model;
            //}
            //var data = _unitOfWork.GetdataByProcedure("SELECT D.SPEC_DELIVERY_ID, O.SPEC_OLIGO_ID " +
            //    " FROM SPEC_DELIVERY D INNER JOIN SPEC_OLIGO O on D.SPEC_DELIVERY_ID=O.SPEC_DELIVERY_ID where D.SPEC_DELIVERY_ID = 309");
            //foreach (var item in data)
            //{
            //    string djkshjksfh = item[0];
            //}

            //Building query based on the input
            string query = Helper.ExpressionCreater<Spec_DeliveryEntity>.QueryBulider(specInputs);
            //Extracting fields from the input
            List<string> fields = specInputs.Select(x => x.FieldName).ToList();
            //Creating dynamic type based on fields
            dynamic reulttype = Helper.ExpressionCreater<Spec_DeliveryEntity>.GetCustomType(fields);
            //Getting dyanmic data based on query and type
            dynamic data = _unitOfWork.GetdataByProcedure(reulttype, query);
            return Helper.ExpressionCreater<Spec_DeliveryEntity>.GetDataList(data);
        }



        /// <summary>
        /// Fetches delivery details by id
        /// </summary>
        /// <param name="DeliveryId"></param>
        /// <returns></returns>
        public BusinessEntities.Spec_DeliveryEntity GetSpecDeliveryById(int DeliveryId)
        {
            var specDelivery = _unitOfWork.SpecDeliveryRepository.GetByID(DeliveryId);
            if (specDelivery != null)
            {
                //Mapping database entities to business entities
                Mapper.CreateMap<SPEC_DELIVERY, Spec_DeliveryEntity>();
                var model = Mapper.Map<SPEC_DELIVERY, Spec_DeliveryEntity>(specDelivery);
                return model;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the deliveries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Spec_DeliveryEntity> GetAllSpecDeliveries()
        {
            var specDeliveries = _unitOfWork.SpecDeliveryRepository.GetAll().ToList();
            if (specDeliveries.Any())
            {
                //Mapping database entities to business entities
                Mapper.CreateMap<SPEC_DELIVERY, Spec_DeliveryEntity>();
                var model = Mapper.Map<List<SPEC_DELIVERY>, List<Spec_DeliveryEntity>>(specDeliveries);
                return model;
            }
            return null;
        }

        /// <summary>
        /// Creates a delivery
        /// </summary>
        /// <param name="specDeliveryEntity"></param>
        /// <returns></returns>
        public int CreateSpecDelivery(Spec_DeliveryEntity specDeliveryEntity)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<Spec_DeliveryEntity, SPEC_DELIVERY>();
                var specDelivery = Mapper.Map<Spec_DeliveryEntity, SPEC_DELIVERY>(specDeliveryEntity);
                specDelivery.CREATE_DTM = DateTime.Now;
                specDelivery.VERSION_DTM = DateTime.Now;
                _unitOfWork.SpecDeliveryRepository.Insert(specDelivery);
                _unitOfWork.Save();
                scope.Complete();
                return specDelivery.SPEC_DELIVERY_ID;
            }
        }

        /// <summary>
        /// Updates a spec delivery
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="specDelivery"></param>
        /// <returns></returns>
        public bool UpdateSpecDelivery(int deliveryId, Spec_DeliveryEntity specDelivery)
        {
            var success = false;
            if (specDelivery != null)
            {
                using (var scope = new TransactionScope())
                {
                    var SpecDeliveryData = _unitOfWork.SpecDeliveryRepository.GetByID(deliveryId);
                    #region Set
                    SpecDeliveryData.CREATE_DTM = specDelivery.CREATE_DTM;
                    SpecDeliveryData.DECLARED_VALUE = specDelivery.DECLARED_VALUE;
                    SpecDeliveryData.DECLARED_VALUE_NATURAL = specDelivery.DECLARED_VALUE_NATURAL;
                    SpecDeliveryData.DIRECTED_TYPE = specDelivery.DIRECTED_TYPE;
                    SpecDeliveryData.DO_SHIP = specDelivery.DO_SHIP;
                    SpecDeliveryData.EXTERNAL_REF_ID = specDelivery.EXTERNAL_REF_ID;
                    SpecDeliveryData.GUAR_COMPLETE_DTM = specDelivery.GUAR_COMPLETE_DTM;
                    SpecDeliveryData.PARENT_SPEC_DELIVERY_ID = specDelivery.PARENT_SPEC_DELIVERY_ID;
                    SpecDeliveryData.PROCESS_PRIORITY = specDelivery.PROCESS_PRIORITY;
                    SpecDeliveryData.PROJECT_NAME = specDelivery.PROJECT_NAME;
                    SpecDeliveryData.REF_ID = specDelivery.REF_ID;
                    SpecDeliveryData.REMAKE = specDelivery.REMAKE;
                    SpecDeliveryData.SPEC_DELIVERY_ID = specDelivery.SPEC_DELIVERY_ID;
                    SpecDeliveryData.SPEC_ORDER_ID = specDelivery.SPEC_ORDER_ID;
                    SpecDeliveryData.SPEC_STATE_ID = specDelivery.SPEC_STATE_ID;
                    SpecDeliveryData.VERSION_DTM = specDelivery.VERSION_DTM;
                    SpecDeliveryData.VERSION_NBR = specDelivery.VERSION_NBR;
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                    #endregion
                    //if (SpecDeliveryData != null)
                    //{
                    //    SpecDeliveryData.SPEC_DELIVERY_ID = specDelivery.SPEC_DELIVERY_ID;
                    //    _unitOfWork.SpecDeliveryRepository.Update(SpecDeliveryData);
                    //    _unitOfWork.Save();
                    //    scope.Complete();
                    //    success = true;
                    //}
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular delivery
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public bool CancelSpecDelivery(List<int> deliveryIds)
        {
            var success = false;
            if (deliveryIds.Count() > 0)
            {
                using (var scope = new TransactionScope())
                {
                    Expression<Func<SPEC_OLIGO, bool>> customLambdaForOligo
                    = ExpressionCreater<SPEC_OLIGO>.GetWhereLambdaForList(deliveryIds, "SPEC_DELIVERY_ID");
                    Expression<Func<SPEC_DELIVERY, bool>> customLambdaForDelivery
                    = ExpressionCreater<SPEC_DELIVERY>.GetWhereLambdaForList(deliveryIds,"SPEC_DELIVERY_ID");
                    var specOligos = _unitOfWork.SpecOligoRepository.GetDataByFilter(customLambdaForOligo).ToList();
                    var specDeliveries = _unitOfWork.SpecDeliveryRepository.GetDataByFilter(customLambdaForDelivery).ToList();
                  
                    if(specOligos.Any())
                        specOligos.ForEach(a => a.SPEC_STATE_ID = 3);
                    if (specDeliveries.Any())
                        specDeliveries.ForEach(a => a.SPEC_STATE_ID = 3);

                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }
            return success;
        }
    }
}
