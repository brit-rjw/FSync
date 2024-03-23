module FSync.DeleteExtraFiles

open FSync.Helpers

let deleteExtraFiles (targetDirPath: string) (sourceDirPath: string) =
    getFilePaths targetDirPath
    |> Array.Parallel.iter (fun path ->
        match fileExists (convertTargetPathToSourcePath path targetDirPath sourceDirPath) with
        | true -> ()
        | false -> deleteFile path)