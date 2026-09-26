Answers:

Controller:
1. Removed direct access to dbcontext
2. changed get all endpoint to use the service layer


Service Interface:
1. Added delete interface method

Service Layer:
1. implemented delete method which calls the repository delete method