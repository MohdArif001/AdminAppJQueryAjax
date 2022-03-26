UPDATE Users SET IsVerified=1,IsActive=1 where Id=1;
UPDATE Users SET Email='irshadali@gmail.com' where Id=1;
select * from Users
Alter Table VerifyAccount alter column UserId varchar(100)
alter table VerifyAccount drop  constraint FK__VerifyAcc__UserI__276EDEB3