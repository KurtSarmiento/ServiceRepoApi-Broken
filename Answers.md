Answers:

Controller:
1. Removed direct access to dbcontext
2. changed get all endpoint to use the service layer
3. moved trimming of name to service layer
4. moved setting of created at to service layer


Service Interface:
1. Added delete interface method

Service Layer:
1. implemented delete method which calls the repository delete method
2. made getallasync an await task
3. made getbyidasync an await task
4. removed createdat edit in the edit function