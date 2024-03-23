module FSync.CopyFiles

open FSync.Helpers

let copyMissingFiles (sourceDirPath: string) (targetDirPath: string) =
    getFilePaths sourceDirPath
    |> Array.Parallel.iter (fun path ->
        let targetPath = convertSourcePathToTargetPath path sourceDirPath targetDirPath
        match fileExists targetPath with
        | true ->
            match isFileSizeSame path targetPath with
            | true ->
                match isFileContentSame path targetPath with
                | true -> ()
                | false -> replaceFile path targetPath
            | false -> replaceFile path targetPath
        | false -> copyFile path targetPath)