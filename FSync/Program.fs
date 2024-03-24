open FSync.Helpers
open FSync.DeleteExtraDirs
open FSync.DeleteExtraFiles
open FSync.CreateMissingDirs
open FSync.CopyFiles



[<EntryPoint>]
let main argv =
    match argv with
    | [| sourceDirPathArg; targetDirPathArg |] ->
        deleteExtraDirs targetDirPathArg sourceDirPathArg
        deleteExtraFiles targetDirPathArg sourceDirPathArg
        createMissingDirs sourceDirPathArg targetDirPathArg
        copyMissingFiles sourceDirPathArg targetDirPathArg
        0
    | _ ->
        printError "Provide a source and target directory path"
        1