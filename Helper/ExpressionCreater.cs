using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using BusinessEntities;
using System.Reflection.Emit;
using System.Data;

namespace Helper
{
    public class ExpressionCreater<TEntity> where TEntity : class
    {
        /// <summary>
        /// Generates where lambda for the input provided to it
        /// </summary>
        /// <param name="filterValue"></param>
        /// <param name="filterField"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> GetWhereLambda(int filterValue,
                                                      string filterField,
                                                      ExpressionCondition condition)
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity), "action");
            Expression firstCondition = GetExpression(param, filterField, filterValue, condition);
            return Expression.Lambda<Func<TEntity, bool>>(firstCondition, param);
        }

        /// <summary>
        /// Get expression for the input provided to it
        /// </summary>
        /// <param name="param"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private static Expression GetExpression(ParameterExpression param,
                                         string property,
                                         int value,
                                         ExpressionCondition condition)
        {
            Expression prop = Expression.Property(param, property);
            Expression val = Expression.Constant(value);
            switch (condition)
            {
                case ExpressionCondition.Equal:
                    return Expression.Equal(prop, val);
                case ExpressionCondition.GreaterThan:
                    return Expression.GreaterThan(prop, val);
                case ExpressionCondition.LessThan:
                    return Expression.LessThan(prop, val);
                case ExpressionCondition.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(prop, val);
                case ExpressionCondition.LessThanOrEqual:
                    return Expression.LessThanOrEqual(prop, val);
            }
            return Expression.Equal(prop, val);
        }

        /// <summary>
        /// Generates a where clause for a field based on list of inputs
        /// </summary>
        /// <param name="filterValues"></param>
        /// <param name="filterField"></param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> GetWhereLambdaForList(List<int> filterValues,
                                                      string filterField)
        {
            var methodInfo = typeof(List<int>).GetMethod("Contains", new Type[] { typeof(int) });
            var list = Expression.Constant(filterValues);
            var param = Expression.Parameter(typeof(TEntity), "action");
            var value = Expression.Property(param, filterField);
            var body = Expression.Call(list, methodInfo, value);
            return Expression.Lambda<Func<TEntity, bool>>(body, param);
        }
        /// <summary>
        /// Builds a dynamic query based on input
        /// </summary>
        /// <param name="specInputs"></param>
        /// <returns></returns>

        public static string QueryBulider(List<Spec_Input> specInputs)
        {
            
            string[] selectFields = specInputs.Select(x => " CAST(" + x.Table + "." + x.FieldName + " AS VARCHAR(100)) AS " + x.FieldName).ToArray();
            string condition = " WHERE ";
            string selectQuery = "SELECT " + string.Join(", ", selectFields) + " FROM ";
            string tablename = string.Empty;
            string fieldOne = string.Empty;
            int cnt = specInputs.Count;
            int loop = 1;
            string finalQuery = string.Empty;
            if (string.IsNullOrEmpty(specInputs.FirstOrDefault().FieldName.Trim()))
            {
                finalQuery = "SELECT CAST(" + specInputs.FirstOrDefault().FieldName + " AS VARCHAR(100)) AS " + specInputs.FirstOrDefault().FieldName + " FROM " +
                    specInputs.FirstOrDefault().Table;
                return finalQuery;
            }
            if (specInputs.FirstOrDefault().Values.Count() == 0)
            {
                finalQuery = "SELECT * FROM " + specInputs.FirstOrDefault().Table;
                return finalQuery;
            }
            if (cnt > 1)
            {
                foreach (var specInput in specInputs)
                {

                    if (tablename != specInput.Table || string.IsNullOrEmpty(tablename))
                    {
                        if (loop > 1)
                        {
                            tablename = tablename + " INNER JOIN " + specInput.Table + " ON "
                                + tablename + "." + "SPEC_DELIVERY_ID" + " = " + specInput.Table + "." + "SPEC_DELIVERY_ID";
                            //+ tablename + "." + fieldOne + " = " + specInput.Table + "." + specInput.FieldName;
                        }
                        else
                        {
                            tablename = specInput.Table;
                        }
                        fieldOne = specInput.FieldName;
                    }
                    if (!string.IsNullOrEmpty(specInput.Condition))
                    {
                        if (specInput.Condition.ToUpper().Contains("BETWEEN"))
                        {
                            if (specInput.Values.Count() < 2 && specInput.Condition.ToUpper().Contains("BETWEEN"))
                                return "Two values required for between operation";

                            if (specInput.Condition.ToUpper().Contains("BETWEEN"))
                            {
                                if (string.IsNullOrEmpty(condition.Trim()))
                                    condition = condition + specInput.Table + "." + specInput.FieldName + " BETWEEN '" +specInput.Values.ElementAt(0) + "'  AND  '" + specInput.Values.ElementAt(1)+"'";
                                else if (condition.Trim().ToUpper() == "WHERE")
                                    condition = condition + " " + specInput.Table + "." + specInput.FieldName + " BETWEEN '" + specInput.Values.ElementAt(0) + "' AND '" + specInput.Values.ElementAt(1) + "'";
                                else
                                    condition = condition + " " + specInput.Operation + " " + specInput.Table + "." + specInput.FieldName + " BETWEEN '" + specInput.Values.ElementAt(0) + "' AND '" + specInput.Values.ElementAt(1) +"'";
                            }
                        }
                        else
                        {
                            if (loop == 1)
                                condition = condition + specInput.Table + "." + specInput.FieldName + " " + specInput.Condition + " '" + specInput.Values.FirstOrDefault() + "'" ;
                            else
                                condition = condition + " " + specInput.Operation + " " + specInput.Table + "." + specInput.FieldName + " " + specInput.Condition + " '" + specInput.Values.FirstOrDefault() + "'"; 
                        }
                    }
                    loop = loop + 1;
                }
                finalQuery = selectQuery + tablename + condition;
            }
            else
            {
                string specField = specInputs.FirstOrDefault().FieldName;
                string specTable = specInputs.FirstOrDefault().Table;
                if (specInputs.FirstOrDefault().Condition.ToUpper().Contains("BETWEEN"))
                {
                    if (specInputs.FirstOrDefault().Values.Count() < 2 &&
                        specInputs.FirstOrDefault().Condition.ToUpper().Contains("BETWEEN"))
                        return "Two values required for between operation";

                    if (specInputs.FirstOrDefault().Condition.ToUpper().Contains("BETWEEN"))
                    {
                        if (string.IsNullOrEmpty(condition.Trim()))
                            condition = condition + specTable + "." + specField + " BETWEEN  '" + specInputs.FirstOrDefault().Values.ElementAt(0) + "'  AND  '" + specInputs.FirstOrDefault().Values.ElementAt(1) + "'"; 
                        else if (condition.Trim().ToUpper() == "WHERE")
                            condition = condition + " " + specInputs.FirstOrDefault().Table + "." + specInputs.FirstOrDefault().FieldName + " BETWEEN '" + specInputs.FirstOrDefault().Values.ElementAt(0) + "'  AND '" + specInputs.FirstOrDefault().Values.ElementAt(1) + "'";
                        else
                            condition = condition + " AND " + specInputs.FirstOrDefault().Table + "." + specInputs.FirstOrDefault().FieldName + " BETWEEN '" + specInputs.FirstOrDefault().Values.ElementAt(0) + "' AND '" + specInputs.FirstOrDefault().Values.ElementAt(1) + "'";
                    }
                }
                else
                {
                    if (condition.Trim() != "WHERE")
                        condition = condition + " AND " + specTable + "." + specField + " " + specInputs.FirstOrDefault().Condition + " '" + specInputs.FirstOrDefault().Values.FirstOrDefault() + "'";
                    else
                        condition = condition + specTable + "." + specField + " " + specInputs.FirstOrDefault().Condition + " '" + specInputs.FirstOrDefault().Values.FirstOrDefault() + "'";
                }
                finalQuery = selectQuery + specTable + condition;
            }
            return finalQuery;
        }

        public static dynamic GetCustomType(List<string> fields)

        {


            TypeBuilder builder = CompositeType.CreateTypeBuilder(
                    "IDTDynamicAssembly", "IDTModule", "IDTType");

            foreach (var field in fields)
            {
                CompositeType.CreateAutoImplementedProperty(builder, field, typeof(string));
            }
            //BusinessEntities.CompositeType.CreateAutoImplementedProperty(builder, "SPEC_OLIGO_ID", typeof(int));
            //BusinessEntities.CompositeType.CreateAutoImplementedProperty(builder, "id", typeof(int));

            dynamic resultType = builder.CreateType();

            return resultType;

        }



        /// <summary>
        /// Creates datatable from dynamic collection
        /// </summary>
        /// <param name="specdeliveries"></param>
        /// <returns></returns>
        public static DataTable GetDataList(dynamic specdeliveries)
        {
            try
            {
                DataTable dt = new DataTable();
                int j = 1;
                List<string> lstr = new List<string>();
                foreach (var item in specdeliveries)
                {
                    var prop = item.GetType().GetProperties();

                    if (j == 1)
                    {
                        for (int i = 0; i < prop.Length; i++)
                        {
                            DataColumn dc = new DataColumn();
                            dc.ColumnName = prop[i].Name;
                            dc.DataType = typeof(string);
                            dt.Columns.Add(dc);
                            lstr.Add(prop[i].Name);
                        }
                    }
                    DataRow dr = dt.NewRow();

                    for (int k = 0; k < lstr.Count; k++)
                    {
                        dr[lstr[k]] = item.GetType().GetProperty(lstr[k]).GetValue(item, null);

                    }

                    dt.Rows.Add(dr);
                    j++;
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }


    
    //public class SPEC_INPUT
    //{
    //    public string Table { get; set; }
    //    public string FieldName { get; set; }
    //    public List<string> Values { get; set; }
    //    public string Condition { get; set; } // < between like
    //    public string Operation { get; set; } //AND
    //}
}
