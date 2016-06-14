using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataAccess;
using DataAccess.UnitOfWork;
using System.Linq.Expressions;
using System;
using Helper;
using System.Data;

namespace BusinessServices
{
    /// <summary>
    /// Offers services for spec oligo specific CRUD operations
    /// </summary>
    public class SpecOligoServices:ISpecOligoServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public SpecOligoServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches oligo by id
        /// </summary>
        /// <param name="oligoId">Oligo id to get its details</param>
        /// <returns></returns>
        public Spec_OligoEntity GetSpecOligoById(int oligoId)
        {
            var specOligo = _unitOfWork.SpecOligoRepository.GetByID(oligoId);
            if (specOligo != null)
            {
                //Mapping database entities to business entities
                Mapper.CreateMap<SPEC_OLIGO, Spec_OligoEntity>();
                var model = Mapper.Map<SPEC_OLIGO, Spec_OligoEntity>(specOligo);
                return model;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the Oligos.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.Spec_OligoEntity> GetAllSpecOligos()
        {
            var specOligo = _unitOfWork.SpecOligoRepository.GetAll().ToList();
            if (specOligo.Any())
            {
                //Mapping database entities to business entities
                Mapper.CreateMap<SPEC_OLIGO, Spec_OligoEntity>();
                var SpecOligosModel = Mapper.Map<List<SPEC_OLIGO>, List<Spec_OligoEntity>>(specOligo);
                return SpecOligosModel;
            }
            return null;
        }

        /// <summary>
        /// Gets spec oligos based on input
        /// </summary>
        /// <param name="specInputs">Enduser inputs</param>
        /// <returns></returns>
        public DataTable GetSpecOligosByFilter(List<Spec_Input> specInputs)
        {
            //Build custom query based on input
            string query = Helper.ExpressionCreater<Spec_OligoEntity>.QueryBulider(specInputs);
            //Extraction of fields from the input
            List<string> fields = specInputs.Select(x => x.FieldName).ToList();
            //Getting dynamic type based on the input fields
            dynamic reulttype = Helper.ExpressionCreater<Spec_OligoEntity>.GetCustomType(fields);
            //Getting dynamic data based on query and type
            dynamic data = _unitOfWork.GetdataByProcedure(reulttype, query);           
            return Helper.ExpressionCreater<Spec_OligoEntity>.GetDataList(data);
        }


        /// <summary>
        /// Creates a SpecOligo
        /// </summary>
        /// <param name="productEntity"></param>
        /// <returns></returns>
        public int CreateSpecOligos(Spec_OligoEntity specOligoEntity)
        {
            using (var scope = new TransactionScope())
            {
                //Mapping database entities to business entities
                Mapper.CreateMap<Spec_OligoEntity, SPEC_OLIGO>();
                var specOligo = Mapper.Map<Spec_OligoEntity, SPEC_OLIGO>(specOligoEntity);
                specOligo.CREATE_DTM = DateTime.Now;
                specOligo.VERSION_DTM = DateTime.Now;
                _unitOfWork.SpecOligoRepository.Insert(specOligo);
                _unitOfWork.Save();
                scope.Complete();
                return specOligo.SPEC_OLIGO_ID;
            }
        }

        /// <summary>
        /// Updates a specOligo
        /// </summary>
        /// <param name="oligoId"></param>
        /// <param name="specOligoEntity"></param>
        /// <returns></returns>
        public bool UpdateSpecOligos(int oligoId, Spec_OligoEntity specOligoEntity)
        {
            var success = false;
            if (specOligoEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    #region Set
                    var specOligo = _unitOfWork.SpecOligoRepository.GetByID(oligoId);
                    specOligo.BRANCH_LOCATION_ID = specOligoEntity.BRANCH_LOCATION_ID;
                    specOligo.CREATE_DTM = specOligoEntity.CREATE_DTM;
                    specOligo.DESCRIPTION = specOligoEntity.DESCRIPTION;
                    specOligo.DIRECTED_TYPE = specOligoEntity.DIRECTED_TYPE;
                    specOligo.EXTERNAL_REF_ID = specOligoEntity.EXTERNAL_REF_ID;
                    specOligo.GUAR_MAX_SYNTH_UOM_ID = specOligoEntity.GUAR_MAX_SYNTH_UOM_ID;
                    specOligo.GUAR_MAX_SYNTH_VAL = specOligoEntity.GUAR_MAX_SYNTH_VAL;
                    specOligo.GUAR_MIN_SHIP_UOM_ID = specOligoEntity.GUAR_MIN_SHIP_UOM_ID;
                    specOligo.GUAR_MIN_SHIP_VAL = specOligoEntity.GUAR_MIN_SHIP_VAL;
                    specOligo.GUAR_MIN_SYNTH_UOM_ID = specOligoEntity.GUAR_MIN_SYNTH_UOM_ID;
                    specOligo.GUAR_MIN_SYNTH_VAL = specOligoEntity.GUAR_MIN_SYNTH_VAL;
                    specOligo.IS_SYNTHESIS_REQUIRED = specOligoEntity.IS_SYNTHESIS_REQUIRED;
                    specOligo.NAME = specOligoEntity.NAME;
                    specOligo.OLIGO_TYPE = specOligoEntity.OLIGO_TYPE;
                    specOligo.PARENT_SPEC_OLIGO_ID = specOligoEntity.PARENT_SPEC_OLIGO_ID;
                    specOligo.PROCESS_PRIORITY = specOligoEntity.PROCESS_PRIORITY;
                    specOligo.PROD_ID = specOligoEntity.PROD_ID;
                    specOligo.PURIFICATION_ID = specOligoEntity.PURIFICATION_ID;
                    specOligo.PURITY_EXPL_ID = specOligoEntity.PURITY_EXPL_ID;
                    specOligo.PURITY_GUAR_PCT = specOligoEntity.PURITY_GUAR_PCT;
                    specOligo.PURITY_GUAR_USABLE = specOligoEntity.PURITY_GUAR_USABLE;
                    specOligo.QUANTITY = specOligoEntity.QUANTITY;
                    specOligo.REF_ID = specOligoEntity.REF_ID;
                    specOligo.SCALE_ID = specOligoEntity.SCALE_ID;
                    specOligo.SEQUENCE = specOligoEntity.SEQUENCE;
                    specOligo.SEQUENCE_CODON = specOligoEntity.SEQUENCE_CODON;
                    specOligo.SEQUENCE_DESCRIPTION = specOligoEntity.SEQUENCE_DESCRIPTION;
                    //specOligo.SPEC_DELIVERY = specOligoEntity.SPEC_DELIVERY;
                    specOligo.SPEC_DELIVERY_ID = specOligoEntity.SPEC_DELIVERY_ID;
                    specOligo.SPEC_OLIGO_ID = specOligoEntity.SPEC_OLIGO_ID;
                    specOligo.SPEC_ORDER_ID = specOligoEntity.SPEC_ORDER_ID;
                    specOligo.SPEC_STATE_ID = specOligoEntity.SPEC_STATE_ID;
                    specOligo.VERSION_DTM = specOligoEntity.VERSION_DTM;
                    specOligo.VERSION_NBR = specOligoEntity.VERSION_NBR;
                    specOligo.WORKFLOW_PATH_ID = specOligoEntity.WORKFLOW_PATH_ID;
                    specOligo.YIELD_EXPL_ID = specOligoEntity.YIELD_EXPL_ID;
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                    return success;
                    # endregion Set
                    //if (specOligo != null)
                    //{
                    //    Mapper.CreateMap<Spec_OligoEntity, SPEC_OLIGO>();
                    //    var newSpecOligo = Mapper.Map<Spec_OligoEntity, SPEC_OLIGO>(specOligoEntity);               
                    //    _unitOfWork.SpecOligoRepository.Update(newSpecOligo);
                    //    _unitOfWork.Save();
                    //    scope.Complete();
                    //    success = true;
                    //}
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular oligo
        /// </summary>
        /// <param name="oligoId"></param>
        /// <returns></returns>
        public bool CancelSpecOligo(List<int> oligoIds)
        {
            var success = false;
            if (oligoIds.Count() > 0)
            {
                using (var scope = new TransactionScope())
                {
                    //Creating custom lamda expression to cancel spec oligo
                    Expression<Func<SPEC_OLIGO, bool>> customLambdaForOligo
                    = ExpressionCreater<SPEC_OLIGO>.GetWhereLambdaForList(oligoIds, "SPEC_OLIGO_ID");
                    var specOligos = _unitOfWork.SpecOligoRepository.GetDataByFilter(customLambdaForOligo).ToList();                    
                    if (specOligos.Any())
                        specOligos.ForEach(a => a.SPEC_STATE_ID = 3); //3-state id for cancellation

                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }
            return success;
        }
    }
}
