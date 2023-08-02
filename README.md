# DFA-Parser
A command-line tool used to test input strings against a user-provided deterministic finite automaton. Users provide the finite state machine using a defined string representation.  
## Usage
DFAs are represented using the following syntax:  
  s1(s2:x, s3:y, ...), s2*(s1:z, s3:x, ...), ...  
    s1,s2,... are state identifiers and may be any string.  
    x,y,z... are tokens in the language.  
    The * character after a state identifier marks the state as a final/accept state.  
    Transitions are listed in parentheses after the state identifier in the form of s2:x, meaning transition to state s2 on an 'x'.  

  ## Example
  Given a DFA, s0*(s0:0, s1:1), s1(s0:1, s2:0), s2(s1:0, s2:1), we would like to see which of the strings 0, 1, 11, 110, 0110 are accepted. To mark the DFA representation in the command-line, we use the -d flag.  
  `dotnet run 0 1 11 110 0110 -d "s0*(s0:0, s1:1), s1(s0:1, s2:0), s2(s1:0, s2:1)"`  
  
  ### Note
  This project was done to familarize myself with C# and the .NET environmnent.  
