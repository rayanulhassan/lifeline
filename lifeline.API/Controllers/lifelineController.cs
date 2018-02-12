using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using lifeline.BOL;
using lifeline.BLL;
using lifeline.DAL;

namespace lifeline.API.Controllers
{
    public class lifelineController : ApiController
    {
        JsonFormattorsObject resObj;
        JsonFormatterList resList;
        membersBs memberObj;
        styleAccessoriesBs stylesAccessoriesObj;
        stylesBs stylesObj;
        foodItemsBs foodItemObj;
        dietPlansBs dietPlanObj;



        public lifelineController()
        {
            resList = new JsonFormatterList();
            resObj = new JsonFormattorsObject();
            memberObj = new membersBs();
            stylesAccessoriesObj = new styleAccessoriesBs();
            stylesObj = new stylesBs();
            foodItemObj = new foodItemsBs();
            dietPlanObj = new dietPlansBs();
        }

        [HttpGet]
        [ActionName("getMemberByEmail")]
        public IHttpActionResult get(string email = "all")
        {
            try
            {
                if (email != "all")
                {
                    var resultObj = memberObj.getByEmail(email);

                    if (resultObj != null)
                    {
                        // resObj.Status = HttpStatusCode.Found;
                        resObj.Status = HttpStatusCode.OK;
                        resObj.Message = "Content Found";
                        resObj.Data = resultObj;
                        // return Content(HttpStatusCode.Found,resObj);
                        return Ok(resObj);
                    }

                    else
                    {
                        resObj.Status = HttpStatusCode.NoContent;
                        resObj.Message = "No data found";
                        resObj.Data = resultObj;
                        return Content(HttpStatusCode.NoContent, resObj);
                    }
                }
                else
                {
                    var resultList = memberObj.getAll().ToList();
                    if (resultList != null)
                    {
                        resList.Status = HttpStatusCode.OK;
                        resList.Message = "Content Found";
                        resList.Data = resultList;
                        resList.DataCount = resultList.Count;
                        return Ok(resList);
                    }

                    else
                    {
                        resList.Status = HttpStatusCode.NoContent;
                        resList.Message = "No data found";
                        resList.Data = resultList;
                        resList.DataCount = resultList.Count;
                        return Content(HttpStatusCode.NoContent, resList);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest("Some error has occured. Error message : " + e.Message);
            }
        }

        [HttpGet]
        [ActionName("getMemberById")]
        public IHttpActionResult get([FromUri]int id)
        {
            try
            {
                if (id == null)
                    throw new Exception("id not defined properly");
                var result = memberObj.getById(id);

                if (result != null)
                {
                    resObj.Status = HttpStatusCode.OK;
                    resObj.Message = "Member Found get";
                    resObj.Data = result;
                    return Ok(resObj);
                    //return Content( HttpStatusCode.Found, res);
                }

                else
                {
                    resObj.Status = HttpStatusCode.NoContent;
                    resObj.Message = "No memberObj found";
                    resObj.Data = result;
                    return Content(HttpStatusCode.NoContent, resObj);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Some error has occured. Error message : " + e.Message);
            }
        }

        [HttpPost]
        [ActionName("postMember")]
        public IHttpActionResult post([FromBody]Members newMember)
        {
            try
            {
                memberObj.insert(newMember);

                if (newMember != null)
                {
                    resObj.Status = HttpStatusCode.Created;
                    resObj.Message = "http://lifeline1.gear.host/api/lifeline/getmemberbyemail?email=" + newMember.email;
                    resObj.Data = newMember;
                    return Created("http://lifeline1.gear.host/api/lifeline/getmemberbyemail?email=" + newMember.email, resObj);
                }

                else
                {
                    resObj.Status = HttpStatusCode.InternalServerError;
                    resObj.Message = "Member not created. Did you provided the required data?";
                    resObj.Data = newMember;
                    return Content(HttpStatusCode.InternalServerError, resObj);
                }

            }
            catch (Exception e)
            {
                return BadRequest("Some error has occured.Did you provided the required data ? Error message : " + e.Message);
            }
        }

        [HttpPut]
        [ActionName("updateMember")]
        public IHttpActionResult put(int id, [FromBody]Members newMember)
        {

            try
            {

                resObj.Status = HttpStatusCode.OK;
                resObj.Message = new Uri(Request.RequestUri + "").ToString();
                resObj.Data = memberObj.update(id, newMember);
                resObj.Data = newMember;

                return Ok(resObj);
            }
            catch (Exception e)
            {

                return BadRequest("Some error has occured. Error Message : " + e.Message);
            }
        }

        [HttpDelete]
        [ActionName("deleteMember")]
        public IHttpActionResult delete([FromUri]string email)
        {
            try
            {
                if (email == null)
                    throw new Exception("email not defined properly");

                memberObj.delete(email);
                resObj.Status = HttpStatusCode.OK;
                resObj.Message = "Required memberObj deleted";
                resObj.Data = null;
                return Ok(resObj);
            }

            catch (Exception e)
            {

                return BadRequest("Some error has occured. Error Message : " + e.Message);
            }
        }

        //[HttpPost]
        //[ActionName("getStyleItemsByTypeAndCategory")]
        //public IHttpActionResult getStyleItemsByTypeAndCategory([FromBody] getStyleItemsByTypeAndCategoryObj obj)
        //{
        //    try
        //    {

        //        string type = obj.type;
        //        string category = obj.category;
        //        if (type == null && category == null)
        //            throw new Exception("Atleast provide value for one variable");


        //        if (type == null)
        //            type = "all";
        //        if (category == null)
        //            category = "all";


        //        if (type != "all" && category != "all")
        //        {
        //            var result = stylesAccessoriesObj.getByTypeAndCategory(type, category).ToList();
        //            if (result.Count != 0)
        //            {
        //                resList.Status = HttpStatusCode.OK;
        //                resList.DataCount = result.Count;
        //                resList.Message = "content found 1";
        //                resList.Data = result;

        //                return Ok(resList);
        //            }
        //            else
        //            {
        //                resList.Status = HttpStatusCode.NotFound;
        //                resList.DataCount = 0;
        //                resList.Message = "no content found";
        //                resList.Data = null;

        //                return Content(HttpStatusCode.NotFound, resList);
        //            }
        //        }
        //        else if (type != "all" && category == "all")
        //        {
        //            var result = stylesAccessoriesObj.getByType(type).ToList();
        //            if (result.Count != 0)
        //            {
        //                resList.Status = HttpStatusCode.OK;
        //                resList.DataCount = result.Count;
        //                resList.Message = "content found";
        //                resList.Data = result;

        //                return Ok(resList);
        //            }
        //            else
        //            {
        //                resList.Status = HttpStatusCode.NotFound;
        //                resList.DataCount = 0;
        //                resList.Message = "no content found";
        //                resList.Data = null;

        //                return Content(HttpStatusCode.NotFound, resList);
        //            }
        //        }

        //        else if (type == "all" && category != "all")
        //        {
        //            var result = stylesAccessoriesObj.getByCategory(category).ToList();
        //            if (result.Count != 0)
        //            {
        //                resList.Status = HttpStatusCode.OK;
        //                resList.DataCount = result.Count;
        //                resList.Message = "content found";
        //                resList.Data = result;

        //                return Ok(resList);
        //            }
        //            else
        //            {
        //                resList.Status = HttpStatusCode.NotFound;
        //                resList.DataCount = 0;
        //                resList.Message = "no content found";
        //                resList.Data = null;

        //                return Content(HttpStatusCode.NotFound, resList);
        //            }
        //        }


        //        else
        //        {
        //            resObj.Status = HttpStatusCode.Forbidden;
        //            resObj.Message = "You have given invalid values for attributes 'type' or 'category'";
        //            resObj.Data = null;

        //            return Content(HttpStatusCode.Forbidden, resObj);
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        return BadRequest("Some error has occured. May be you didn't provide values for attributes. Error Message : " + e.Message);
        //    }
        //}

        [HttpPost]
        [ActionName("getSunglassesSuggestions")]
        public IHttpActionResult getSunglassesSuggestions([FromBody] getSunglassesSuggestionsObj obj)
        {
            try
            {
                if (obj.faceShape == null || obj.gender == null)
                throw new Exception("faceShape or gender not defined properly");

            
                resList.Data = stylesAccessoriesObj.getSunglassessSuggestions(obj.faceShape, obj.gender).ToList();
                resList.DataCount = stylesAccessoriesObj.getSunglassessSuggestions(obj.faceShape, obj.gender).ToList().Count;
                resList.Status = HttpStatusCode.OK;
                resList.Message = "content found";
                return Ok(resList);
            }

            catch (Exception e)
            {
                return BadRequest("no content found. Error message : " + e.Message);
            }
        }

        //[HttpPost]
        //[ActionName("postMemberStyleSuggestions")]
        //public IHttpActionResult postMemberSuggestions([FromBody] postMemberStyleSuggestionObj obj)
        //{
        //    if (obj.memberId <= 0 || obj.styleAccessoriesIds == null)
        //    {
        //        return Content(HttpStatusCode.BadRequest, "Either memberId or stylesAccessoriesId array is null");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            stylesObj.insertRange(obj.memberId, obj.styleAccessoriesIds.ToArray());
        //            resList.Status = HttpStatusCode.OK;
        //            resList.Message = "Suggestion posted";
        //            resList.Data = stylesObj.getByMemberId(obj.memberId,true,true).ToList();
        //            resList.DataCount = stylesObj.getByMemberId(obj.memberId,false,false).ToList().Count;
        //            return Ok(resList);
        //        }
        //        catch (Exception e)
        //        {

        //            return BadRequest("Some error has occured. Error : " + e.Message);
        //        }

        //    }
        //}
        
        //[HttpPost]
        //[ActionName("updateMemberStyleSuggestions")]
        //public IHttpActionResult updateMemberSuggestions([FromBody] postMemberStyleSuggestionObj obj)
        //{
        //    if (obj.memberId <= 0 || obj.styleAccessoriesIds == null)
        //    {
        //        return Content(HttpStatusCode.BadRequest, "Either memberId or stylesAccessoriesId array is null");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            stylesObj.updateMemberIdRange(obj.memberId, obj.styleAccessoriesIds.ToArray(),true,true);
        //            resList.Status = HttpStatusCode.OK;
        //            resList.Message = "Suggestion posted";
        //            resList.Data = stylesObj.getByMemberId(obj.memberId,true,true).ToList();
        //            resList.DataCount = stylesObj.getByMemberId(obj.memberId,false,false).ToList().Count;
        //            return Ok(resList);
        //        }
        //        catch (Exception e)
        //        {

        //            return BadRequest("Some error has occured. Error : " + e.Message);
        //        }
        //    }
        //}

        [HttpPost]
        [ActionName("getHairStyleSuggestions")]
        public IHttpActionResult getHairStyleSuggestions([FromBody] getHairStyleSuggestionsObj obj)
        {

            try
            {
                if (obj.faceShape == null || obj.gender == null || obj.hairType == null)
                    throw new Exception("faceShape or gender not defined properly");

                resList.Data = stylesAccessoriesObj.getHairStyleSuggesstions(obj.faceShape, obj.gender).ToList();
                resList.DataCount = stylesAccessoriesObj.getHairStyleSuggesstions(obj.faceShape, obj.gender).ToList().Count;
                resList.Status = HttpStatusCode.OK;
                if (obj.gender != "")
                {
                    switch (obj.hairType)
                    {
                        case "Straight Hair":
                            resList.Message = "If your hair is long, as noted, a blunt base is best with a sweeping or heavy-straight bang." +
                                              "\nWith medium, or shoulder length, the Bob is best.Any variation will work—from a classic blunt to a more graduated style." +
                                              "\nAs for updos, a classic or low ponytail is timeless and works for almost any occasion";
                            break;
                        case "Wavy Hair":
                            resList.Message = "For women with hair that falls way below the shoulders, your most flattering cut is long, graduated layers with a side-angled or sweeping bang." +
                                               "\nLike long hair, medium or shoulder length looks best with long layers." +
                                               "\nShort waves, or hair that’s ear to chin length, should opt for a one-length cut." +
                                               "\nAs for updos, the experts agree that a loose chignon with a few wavy pieces falling around the face, and a half - up half - down style are head turners.";
                            break;

                        case "Curly Hair":
                            resList.Message = "For Curly hairs you can try Long, Kinky-Curly Layers. To achieve ideal curls, you need first to blow dry your locks, smoothing them slightly with a round bristle brush, and then shape the curls with a curling iron.";
                            break;

                        default:
                            throw new Exception("hairType not defined properly");
                            resList.Message = "";
                    }
                }
                else
                {
                    resList.Message = "";
                }


                return Ok(resList);
            }

            catch (Exception e)
            {

                return Content(HttpStatusCode.BadRequest, "no content found. Error message : " + e.Message);
            }
        }

        //[HttpPost]
        //[ActionName("getMemberStyleSuggestions")]
        //public IHttpActionResult getMemberStyleSuggestions([FromBody] getMemberStyleSuggestions obj)
        //{
        //    try
        //    {
        //        if (obj.type == null)
        //            throw new Exception("Either memberId or type is not set properly");
        //        List<Styles> list = stylesObj.getByMemberIdAndType(obj.id, obj.type,false,true).ToList();

        //        resList.Data = responseFormattors.memberStyleSuggestionformattor(list);
        //        if (list.Count == 0)
        //            resList.Message = "no content found";
        //        else
        //            resList.Message = "content found";

        //        resList.DataCount = list.Count;
        //        resList.Status = HttpStatusCode.OK;
        //        return Ok(resList);
        //    }

        //    catch (Exception e)
        //    {
        //        return BadRequest("no content found. Error message : " + e.Message + " " + obj.id + " " + obj.type);
        //    }

        //}

        [HttpPost]
        [ActionName("getFootwareSuggestions")]
        public IHttpActionResult getFootwareSuggestions([FromBody] getFootwareSuggestionsObj obj)
        {
            try
            {
                if (obj.dressTone == null || obj.dressType == null || obj.gender == null)
                    throw new Exception("dressTone, dressType or gender is null");

                List<Style_Accessories> result = stylesAccessoriesObj.getFootwareSuggestions(obj.dressTone, obj.dressType, obj.gender).ToList();
                if (result.Count == 0)
                {
                    resList.Status = HttpStatusCode.NoContent;
                    resList.Message = "No content found";
                    resList.Data = result;
                    resList.DataCount = result.Count;
                    return Content(HttpStatusCode.NoContent, resList);
                }
                else
                {
                    resList.Status = HttpStatusCode.OK;
                    resList.Message = "content found";
                    resList.Data = result;
                    resList.DataCount = result.Count;
                    return Content(HttpStatusCode.OK, resList);
                }

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, "no content found. Error message : " + e.Message);
            }
        }

        [HttpPost]
        [ActionName("getAllSkincareTips")]
        public IHttpActionResult getAllSkincareTips([FromBody] getSkinCareTipsObj obj)
        {
            try
            {
                if (obj.type != "skin care")
                    throw new Exception("type not defined properly");

                List<Style_Accessories> list = stylesAccessoriesObj.getByType(obj.type).ToList();

                if(list.Count == 0)
                {
                    resList.Message = "No posts are available for display";
                    resList.Data = null;
                    resList.DataCount = 0;
                    resList.Status = HttpStatusCode.NoContent;
                    return Content(HttpStatusCode.NoContent, resList);
                }
                else
                {
                    resList.Message = "content found";
                    if (obj.id == -1)
                    {
                        resList.DataCount = list.Count;
                        resList.Data = list;
                    }

                    else
                    {
                        resList.DataCount = 1;
                        resList.Data = list.Where(x => x.styleAccessoriesId == obj.id).First();
                    }                  

                    
                    resList.Status = HttpStatusCode.OK;
                    return Ok(resList);
                }                    
            }

            catch(Exception e)
            {
                return Content(HttpStatusCode.BadRequest, "Some error has occured. Error message : " + e.Message);
            }
            

        }

        [HttpPost]
        [ActionName("getClothingSuggestions")]
        public IHttpActionResult getClothingSuggestions(getClothingSuggesionsObj obj)
        {
            try
            {
                if (obj.age == null || obj.category == null || obj.gender == null || obj.height == null || obj.weight == null || obj.skinColor == null)
                    throw new Exception("ap ne obj ghalat bnaya ha");

                List<Style_Accessories> result = stylesAccessoriesObj.getClothsSuggestions(obj.gender, obj.weight, obj.category, obj.age).ToList();
                if (result.Count == 0)
                    return Content(HttpStatusCode.NoContent, "No results found");
                resList.Data = result;
                resList.DataCount = result.Count;
                resList.Message = "content found";
                resList.Status = HttpStatusCode.OK;

                return Ok(resList);               

            }
            catch (Exception e)
            {
                return BadRequest(" ap ne chuss mari hai. Error essage : " + e.Message);
            }
        }

        [HttpPost]
        [ActionName("getFoodItemsByCategory")]
        public IHttpActionResult getFoodItemsByCategory(getFoodItemByCategoryObj obj)
        {
            try
            {
                if (obj.name == null)
                    throw new Exception("name not set");
                List<Food_Items> result = foodItemObj.getByCategory(obj.name).ToList();
                if (result.Count == 0)
                    return Content(HttpStatusCode.NoContent, "No content found under this category");

                resList.Status = HttpStatusCode.OK;
                resList.Data = result;
                resList.DataCount = result.Count;
                resList.Message = "Content found";

                return Ok(resList);
        }
            catch(Exception e)
            {
                return Content(HttpStatusCode.BadRequest, "Some error has occured. Error message : " + e.Message);
    }
}

        [HttpPost]
        [ActionName("getFoodItemByName")]
        public IHttpActionResult getFoodItemByName(getFoodItemByCategoryObj obj)
        {
            try
            {
                if (obj.name == null)
                    throw new Exception("name not set");
                Food_Items result = foodItemObj.getItemByName(obj.name);
                if (result == null)
                    return Content(HttpStatusCode.NoContent, "No content found under this name");

                resObj.Status = HttpStatusCode.OK;
                resObj.Data = result;
                resObj.Message = "Content found";

                return Ok(resObj);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, "Some error has occured. Error message : " + e.Message);
            }
        }

        [HttpPost]
        [ActionName("BMICalculator")]
        public IHttpActionResult BMICalculator(BMICalculatorObj obj)
        {
            try
            {
                if ( obj.height <= 0 || obj.weight <= 0)
                    return BadRequest("height or weight is null");

                Dictionary<string,object> result = dietPlanObj.BMICalculator(obj.weight, obj.height);
                
                resObj.Data = result;
                resObj.Message = "BMI calculated";
                resObj.Status = HttpStatusCode.OK;
                return Ok(resObj);
            }
            catch(Exception e)
            {
                return BadRequest("Some error has occured. Error message : " + e.Message);
            }
        }

        [HttpPost]
        [ActionName("dietPlanCreator")]
        public IHttpActionResult dietPlanCreator(dietPlanCreatorObj obj)
        {
            try
            {
                if (obj.weight > 0 || obj.height > 0 || obj.gender == null || obj.age > 0 || obj.activityFactor > 0)
                    throw new Exception("Some error in body values");

                Dictionary<string, object> result = dietPlanObj.planCreator(obj.weight, obj.height, obj.age, obj.activityFactor, obj.gender);
                resList.Data = result;
                resList.DataCount = result.Count;
                resList.Message = "Plans created";
                resList.Status = HttpStatusCode.OK;
                return Ok(resList);
            }
            catch(Exception e)
            {
                resObj.Data = null;
                resObj.Message = "some error has accoured. Error message : " + e.Message;
                resObj.Status = HttpStatusCode.BadRequest;
                return Content(HttpStatusCode.BadRequest, resObj);
            }
            
        }
        

    }
}
