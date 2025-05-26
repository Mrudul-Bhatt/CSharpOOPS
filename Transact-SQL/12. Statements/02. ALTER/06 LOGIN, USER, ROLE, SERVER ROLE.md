In Transact-SQL, managing security is paramount. The `ALTER` statements for `LOGIN`, `USER`, `ROLE`, and `SERVER ROLE` are fundamental for controlling who can access your SQL Server instance and what they can do within databases. This deep dive will cover their syntax, key uses, and provide practical code examples.

### Fundamental Security Concepts

Before diving into `ALTER` statements, let's briefly review the hierarchy:

* **Logins:** Authenticate a principal (person or application) to the SQL Server *instance*. They operate at the server level.
* **Users:** Authorize a login to access a specific *database*. They map to logins and operate at the database level.
* **Server Roles:** Fixed or custom roles that grant server-level permissions (e.g., `sysadmin`, `securityadmin`, `dbcreator`).
* **Database Roles:** Fixed or custom roles that grant database-level permissions (e.g., `db_owner`, `db_datareader`, `db_datawriter`).

### 1. `ALTER LOGIN`

`ALTER LOGIN` is used to modify properties of an existing SQL Server login. These properties include passwords, default databases, password policy settings, and the ability to enable/disable the login.

**Syntax:**

```sql
ALTER LOGIN login_name
{
    ENABLE | DISABLE
  | WITH PASSWORD = 'password' [ HASHED | SHA_512 ]
              [ , MUST_CHANGE = { ON | OFF } ]
              [ , UNLOCK ]
  | WITH DEFAULT_DATABASE = database_name
  | WITH DEFAULT_LANGUAGE = language_name
  | ADD CREDENTIAL credential_name
  | DROP CREDENTIAL credential_name
  | WITH CHECK_EXPIRATION = { ON | OFF }
  | WITH CHECK_POLICY = { ON | OFF }
} [ ; ]
```

**Key Uses and Code Examples:**

**Example 1: Change a SQL Server Login Password**

```sql
-- First, create a sample SQL Login (if it doesn't exist)
CREATE LOGIN TestUser WITH PASSWORD = 'OldPassword123!', CHECK_POLICY = ON;
GO

-- Change the password for 'TestUser'
ALTER LOGIN TestUser WITH PASSWORD = 'NewPassword456!';
GO

-- Verify you can log in with the new password
-- You would typically test this by opening a new SSMS connection
-- and trying to log in as TestUser with 'NewPassword456!'.
```

**Example 2: Force Password Change on Next Login (`MUST_CHANGE`)**

```sql
-- Change password and force user to change it on next login
ALTER LOGIN TestUser WITH PASSWORD = 'TempPassword!', MUST_CHANGE = ON;
GO

-- When 'TestUser' attempts to log in next, SQL Server will prompt them to change the password.
```

**Example 3: Enable/Disable a Login**

Disabling a login prevents it from connecting to the SQL Server instance.

```sql
-- Disable the login
ALTER LOGIN TestUser DISABLE;
GO

-- Try to log in as TestUser now (it should fail).

-- Enable the login
ALTER LOGIN TestUser ENABLE;
GO

-- Test logging in again (should succeed with the current password).
```

**Example 4: Unlock a Login**

If a login is locked out due to too many failed password attempts (when `CHECK_POLICY` is `ON` and password policy is enforced by the OS), you can unlock it.

```sql
-- Simulate a locked login (requires password policy to be enabled and failed attempts)
-- For demonstration, assume TestUser is locked out.
ALTER LOGIN TestUser WITH UNLOCK;
GO
```

**Example 5: Change Default Database**

When `TestUser` logs in, they will automatically connect to `MyDatabase` instead of `master` or their previous default.

```sql
-- Create a sample database
CREATE DATABASE MyDatabase;
GO

-- Change default database for 'TestUser'
ALTER LOGIN TestUser WITH DEFAULT_DATABASE = MyDatabase;
GO

-- Verify (login as TestUser and check current database)
-- SELECT DB_NAME();
```

**Example 6: Control Password Policy and Expiration Checks**

`CHECK_POLICY` and `CHECK_EXPIRATION` relate to Windows password policies.

```sql
-- Disable password policy checks for 'TestUser' (less secure, generally not recommended)
ALTER LOGIN TestUser WITH CHECK_POLICY = OFF;
GO

-- Disable password expiration checks (less secure, generally not recommended)
ALTER LOGIN TestUser WITH CHECK_EXPIRATION = OFF;
GO

-- Re-enable for security (recommended)
ALTER LOGIN TestUser WITH CHECK_POLICY = ON, CHECK_EXPIRATION = ON;
GO
```

### 2. `ALTER USER`

`ALTER USER` is used to modify properties of a database user within a specific database.

**Syntax:**

```sql
ALTER USER user_name
{
    WITH PASSWORD = 'password' -- For SQL Server authenticated users, not Windows users mapped to logins
  | WITH DEFAULT_SCHEMA = schema_name
  | WITH LOGIN = login_name -- To re-map a user to a different login
  | WITH NAME = new_user_name -- To rename the user
} [ ; ]
```

**Key Uses and Code Examples:**

**Example 1: Map a User to a Different Login**

This is useful if you need to change the underlying login for a database user without recreating the user (thus preserving their permissions).

```sql
USE MyDatabase;
GO

-- Create a login and user
CREATE LOGIN OldLogin WITH PASSWORD = 'Password123!';
CREATE USER MyUser FOR LOGIN OldLogin;
GO

-- Create a new login
CREATE LOGIN NewLogin WITH PASSWORD = 'Password456!';
GO

-- Change the user's mapping to the new login
ALTER USER MyUser WITH LOGIN = NewLogin;
GO

-- Verify the mapping (should show NewLogin's SID)
SELECT dp.name AS UserName, sp.name AS LoginName, dp.sid, sp.sid
FROM sys.database_principals dp
JOIN sys.server_principals sp ON dp.sid = sp.sid
WHERE dp.name = 'MyUser';
```

**Example 2: Change Default Schema for a User**

When `MyUser` creates objects without specifying a schema, they will be created in `MySchema`. Also, when `MyUser` logs in and doesn't explicitly select a schema, this will be their default.

```sql
USE MyDatabase;
GO

-- Create a schema
CREATE SCHEMA MySchema;
GO

-- Set 'MyUser's default schema
ALTER USER MyUser WITH DEFAULT_SCHEMA = MySchema;
GO

-- Verify (login as MyUser, then SELECT SCHEMA_NAME())
-- SELECT SCHEMA_NAME();
```

**Example 3: Rename a Database User**

```sql
USE MyDatabase;
GO

-- Rename MyUser to ApplicationUser
ALTER USER MyUser WITH NAME = ApplicationUser;
GO

-- Verify the rename
SELECT name FROM sys.database_principals WHERE name = 'ApplicationUser';
```

### 3. `ALTER ROLE` (Database Role)

`ALTER ROLE` is used to manage custom database roles. This primarily involves adding or removing members from the role.

**Syntax:**

```sql
ALTER ROLE database_role_name
{ ADD MEMBER database_principal | DROP MEMBER database_principal } [ ; ]
```

**Key Uses and Code Examples:**

**Example 1: Add/Remove Members from a Database Role**

```sql
USE MyDatabase;
GO

-- Create a custom database role
CREATE ROLE AppDevelopers;
GO

-- Create some users
CREATE USER Dev1 WITHOUT LOGIN;
CREATE USER Dev2 WITHOUT LOGIN;
GO

-- Add Dev1 to the AppDevelopers role
ALTER ROLE AppDevelopers ADD MEMBER Dev1;
GO

-- Add Dev2 to the AppDevelopers role
ALTER ROLE AppDevelopers ADD MEMBER Dev2;
GO

-- Verify role membership
SELECT dp.name AS MemberName, dr.name AS RoleName
FROM sys.database_role_members AS drm
JOIN sys.database_principals AS dp ON drm.member_principal_id = dp.principal_id
JOIN sys.database_principals AS dr ON drm.role_principal_id = dr.principal_id
WHERE dr.name = 'AppDevelopers';

-- Remove Dev2 from the AppDevelopers role
ALTER ROLE AppDevelopers DROP MEMBER Dev2;
GO

-- Verify removal
SELECT dp.name AS MemberName, dr.name AS RoleName
FROM sys.database_role_members AS drm
JOIN sys.database_principals AS dp ON drm.member_principal_id = dp.principal_id
JOIN sys.database_principals AS dr ON drm.role_principal_id = dr.principal_id
WHERE dr.name = 'AppDevelopers';
```

### 4. `ALTER SERVER ROLE`

`ALTER SERVER ROLE` is used to manage custom server roles (SQL Server 2012+) or to add/remove logins from fixed server roles (like `sysadmin`, `securityadmin`).

**Syntax:**

```sql
ALTER SERVER ROLE server_role_name
{ ADD MEMBER server_principal | DROP MEMBER server_principal } [ ; ]
```

**Key Uses and Code Examples:**

**Example 1: Add/Remove Login from a Fixed Server Role**

Adding a login to `sysadmin` makes that login a full administrator of the SQL Server instance. This is very powerful and should be used with extreme caution.

```sql
USE master;
GO

-- Create a new login for testing
CREATE LOGIN AdminTestLogin WITH PASSWORD = 'Password123!', CHECK_POLICY = OFF;
GO

-- Add AdminTestLogin to the sysadmin fixed server role
ALTER SERVER ROLE sysadmin ADD MEMBER AdminTestLogin;
GO

-- Verify membership (should show AdminTestLogin as a member of sysadmin)
SELECT sp.name AS LoginName, srp.name AS ServerRoleName
FROM sys.server_role_members AS srm
JOIN sys.server_principals AS sp ON srm.member_principal_id = sp.principal_id
JOIN sys.server_principals AS srp ON srm.role_principal_id = srp.principal_id
WHERE srp.name = 'sysadmin';

-- Remove AdminTestLogin from the sysadmin role
ALTER SERVER ROLE sysadmin DROP MEMBER AdminTestLogin;
GO

-- Verify removal
SELECT sp.name AS LoginName, srp.name AS ServerRoleName
FROM sys.server_role_members AS srm
JOIN sys.server_principals AS sp ON srm.member_principal_id = sp.principal_id
JOIN sys.server_principals AS srp ON srm.role_principal_id = srp.principal_id
WHERE srp.name = 'sysadmin';
```

**Example 2: Add/Remove Members from a User-Defined Server Role (SQL Server 2012+)**

```sql
USE master;
GO

-- Create a custom server role (SQL Server 2012+ feature)
CREATE SERVER ROLE ServerAuditors;
GO

-- Create a login for auditing
CREATE LOGIN AuditLogin WITH PASSWORD = 'AuditPassword!', CHECK_POLICY = OFF;
GO

-- Add AuditLogin to the custom ServerAuditors role
ALTER SERVER ROLE ServerAuditors ADD MEMBER AuditLogin;
GO

-- Grant some server-level permissions to the custom role (e.g., VIEW SERVER STATE)
GRANT VIEW SERVER STATE TO ServerAuditors;
GO

-- Verify membership
SELECT sp.name AS MemberName, srp.name AS RoleName
FROM sys.server_role_members AS srm
JOIN sys.server_principals AS sp ON srm.member_principal_id = sp.principal_id
JOIN sys.server_principals AS srp ON srm.role_principal_id = srp.principal_id
WHERE srp.name = 'ServerAuditors';

-- Remove AuditLogin
ALTER SERVER ROLE ServerAuditors DROP MEMBER AuditLogin;
GO
```

### Important Considerations for Interviews:

* **Principle of Least Privilege (PoLP):** Always grant only the necessary permissions. Avoid `sysadmin` unless absolutely required.
* **Authentication vs. Authorization:** Explain that logins handle authentication (who you are), while users and roles handle authorization (what you can do).
* **Windows vs. SQL Logins:** Understand the differences and when to use each.
* **Orphaned Users:** If a login is dropped but the corresponding user in a database remains, that user becomes "orphaned." You'll need to re-map it using `ALTER USER ... WITH LOGIN` or drop and recreate.
* **Security Best Practices:** Discuss enforcing strong passwords, using password policy, and rotating passwords.
* **Custom Roles:** Emphasize the benefits of custom roles for better security management and maintainability compared to granting individual permissions.
* **Default Schema:** Explain its impact on object creation and resolution.
* **Renaming:** Be aware that renaming objects can sometimes cause issues if other scripts or applications refer to the old name.

By demonstrating a strong understanding of these `ALTER` commands and their security implications, you'll show proficiency in SQL Server administration and best practices.

### Cleanup:

```sql
USE MyDatabase;
GO
DROP USER IF EXISTS ApplicationUser;
DROP USER IF EXISTS MyUser;
DROP USER IF EXISTS Dev1;
DROP USER IF EXISTS Dev2;
DROP ROLE IF EXISTS AppDevelopers;
DROP SCHEMA IF EXISTS MySchema;
GO

USE master;
GO
DROP LOGIN IF EXISTS TestUser;
DROP LOGIN IF EXISTS OldLogin;
DROP LOGIN IF EXISTS NewLogin;
DROP LOGIN IF EXISTS AdminTestLogin;
DROP LOGIN IF EXISTS AuditLogin;

IF EXISTS (SELECT * FROM sys.server_principals WHERE name = 'ServerAuditors' AND type = 'SR')
BEGIN
    REVOKE VIEW SERVER STATE FROM ServerAuditors; -- Revoke permissions before dropping
    DROP SERVER ROLE ServerAuditors;
END
GO

DROP DATABASE IF EXISTS MyDatabase;
GO
```