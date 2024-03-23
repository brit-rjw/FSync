open FSync.Helpers
open FSync.DeleteExtraDirs
open FSync.DeleteExtraFiles
open FSync.CreateMissingDirs
open FSync.CopyFiles

[<EntryPoint>]
let main argv =
    
    // Check that there are at least two arguments
    checkArgvArrayCount argv
    
    // Check that the source and target directory paths exist
    checkArgvDirPaths argv
    
    // Paths
    let sourceDirPath = argv[0]
    let targetDirPath = argv[1]
    
    // Delete extra directories
    deleteExtraDirs targetDirPath sourceDirPath
    
    // Delete extra files
    deleteExtraFiles targetDirPath sourceDirPath
    
    // Create missing directories
    createMissingDirs sourceDirPath targetDirPath
    
    // Copy missing files
    copyMissingFiles sourceDirPath targetDirPath
    
    0