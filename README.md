# VERY SIMPLE CRUD API USING .NET CORE
web API for simple todo list using .Net Core

Route which used: https://localhost:{port}/api/ToDo
- GET: api/ToDo 
  => to GET ALL DATA
- GET: api/ToDo/id/{id} 
  => to GET Specific data by ID
- GET: api/ToDo/date/{StartDate}/{EndDate?} 
  => to GET Specific data for next day/current week/specific date
  Ex: api/ToDo/date/yyyy-mm-dd/yyyy-mm-dd
  Note: EndDate is optional Param
- PUT api/ToDo/{id}
  => to Update data by ID
- POST api/ToDo
  => create new data
- DELETE  api/ToDo/{id}
  => delete data by ID
  
NOTE: column isDone will be automatic True if the percentage 100
  
