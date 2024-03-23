module FSync.DeleteExtraDirs

open FSync.Helpers

let deleteExtraDirs (targetDirPath: string) (sourceDirPath: string) =
    getDirPaths targetDirPath
    |> Array.iter (fun path ->
        match dirExists (convertTargetPathToSourcePath path targetDirPath sourceDirPath) with
        | true -> ()
        | false -> deleteDir path)