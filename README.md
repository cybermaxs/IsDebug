# IsDebug
Check presence of DebuggableAttribute.IsJITOptimizerDisabled in your assemblies

Why ?
-------------
A developper could edit your project settings, your build process could be broken or you can have a nuget coming from your private nuget feed, ... There are several reasons to see one or more assemblies compiled in 'Debug' on production. It's a very bad practice and this tools should help to avoid this. Quite basicaly, jsut check presence of DebuggableAttribute.IsJITOptimizerDisabled in your assemblies.

How to use
-----------
`
	> isdebug [OPTIONS] PATH1 PATH2 
`

OPTIONS :
- -r : reccursive (default is false)
- -d : display scan details (default is false)
- -h : Help text