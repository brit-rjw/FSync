module FSync.CreateMissingDirs

open FSync.Helpers

let createMissingDirs (sourceDirPath: string) (targetDirPath: string) =
    getDirPaths sourceDirPath
    |> Array.iter (fun path ->
        let targetPath = convertSourcePathToTargetPath path sourceDirPath targetDirPath
        match dirExists targetPath with
        | true -> ()
        | false -> createDir targetPath)