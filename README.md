# Email Service Test

### Problem
This current implementation of an email service works most of the time. In the production environment, it has been observed crashing  with one of two types of failures. 

The first is a `Connection Failed` exception. This occurs randomly and users are complaining they need to implement retry mechanisms and try many times.

The second exception is a `Unexpected Error` exception. Whenever this exception shows up in the logs, the service crashes and needs to be started again.

One of our developers has attempted to solve the crashes by introducing a Semaphore and a test to fix the issue. Much to their dismay, the problems just became less frequent, but still occur.

### Outline

These problems should be rewritten into failing tests replicating the reported failures. Once these tests are in place, code modifications to make the tests pass are expected.

The included MockEmailClient is not to be modified.

### Running the solution

First you need a working installation of dotnet core https://www.microsoft.com/net/learn/get-started/windows

To get the API up and running, the following dotnet command line command should start the project:
```
dotnet run --project EmailService
```

To run the tests:
```
dotnet test EmailServiceTests
```

### Submission
Once completed, please have the code available for your technical interview as a fork of this project.
