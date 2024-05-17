﻿namespace PolarisLite.Web.ComponentsJS.CKEditor;
using System.Collections.Generic;

public static class EditorCommandsExtensions
{
    private static readonly Dictionary<EditorCommands, string> _values = new()
    {
        { EditorCommands.AddColumnAfter, "addColumnAfter" },
        { EditorCommands.AddColumnBefore, "addColumnBefore" },
        { EditorCommands.AddRowAfter, "addRowAfter" },
        { EditorCommands.AddRowBefore, "addRowBefore" },
        { EditorCommands.Blur, "blur" },
        { EditorCommands.ClearContent, "clearContent" },
        { EditorCommands.ClearNodes, "clearNodes" },
        { EditorCommands.CreateParagraphNear, "createParagraphNear" },
        { EditorCommands.Cut, "cut" },
        { EditorCommands.DeleteColumn, "deleteColumn" },
        { EditorCommands.DeleteCurrentNode, "deleteCurrentNode" },
        { EditorCommands.DeleteNode, "deleteNode" },
        { EditorCommands.DeleteRange, "deleteRange" },
        { EditorCommands.DeleteRow, "deleteRow" },
        { EditorCommands.DeleteSelection, "deleteSelection" },
        { EditorCommands.DeleteTable, "deleteTable" },
        { EditorCommands.Enter, "enter" },
        { EditorCommands.ExitCode, "exitCode" },
        { EditorCommands.ExtendMarkRange, "extendMarkRange" },
        { EditorCommands.First, "first" },
        { EditorCommands.FixTables, "fixTables" },
        { EditorCommands.Focus, "focus" },
        { EditorCommands.GoToNextCell, "goToNextCell" },
        { EditorCommands.GoToPreviousCell, "goToPreviousCell" },
        { EditorCommands.InsertContent, "insertContent" },
        { EditorCommands.InsertContentAt, "insertContentAt" },
        { EditorCommands.InsertTable, "insertTable" },
        { EditorCommands.JoinBackward, "joinBackward" },
        { EditorCommands.JoinDown, "joinDown" },
        { EditorCommands.JoinForward, "joinForward" },
        { EditorCommands.JoinItemBackward, "joinItemBackward" },
        { EditorCommands.JoinItemForward, "joinItemForward" },
        { EditorCommands.JoinUp, "joinUp" },
        { EditorCommands.KeyboardShortcut, "keyboardShortcut" },
        { EditorCommands.Lift, "lift" },
        { EditorCommands.LiftEmptyBlock, "liftEmptyBlock" },
        { EditorCommands.LiftListItem, "liftListItem" },
        { EditorCommands.MergeCells, "mergeCells" },
        { EditorCommands.MergeOrSplit, "mergeOrSplit" },
        { EditorCommands.NewLineInCode, "newlineInCode" },
        { EditorCommands.Redo, "redo" },
        { EditorCommands.RemoveEmptyTextStyle, "removeEmptyTextStyle" },
        { EditorCommands.ResetAttributes, "resetAttributes" },
        { EditorCommands.ScrollIntoView, "scrollIntoView" },
        { EditorCommands.SelectAll, "selectAll" },
        { EditorCommands.SelectNodeBackward, "selectNodeBackward" },
        { EditorCommands.SelectNodeForward, "selectNodeForward" },
        { EditorCommands.SelectParentNode, "selectParentNode" },
        { EditorCommands.SelectTextBlockEnd, "selectTextblockEnd" },
        { EditorCommands.SelectTextBlockStart, "selectTextblockStart" },
        { EditorCommands.SetBlockquote, "setBlockquote" },
        { EditorCommands.SetBold, "setBold" },
        { EditorCommands.SetCellAttribute, "setCellAttribute" },
        { EditorCommands.SetCellSelection, "setCellSelection" },
        { EditorCommands.SetColor, "setColor" },
        { EditorCommands.SetContent, "setContent" },
        { EditorCommands.SetHardBreak, "setHardBreak" },
        { EditorCommands.SetHeading, "setHeading" },
        { EditorCommands.SetHighlight, "setHighlight" },
        { EditorCommands.SetHorizontalRule, "setHorizontalRule" },
        { EditorCommands.SetItalic, "setItalic" },
        { EditorCommands.SetLink, "setLink" },
        { EditorCommands.SetMark, "setMark" },
        { EditorCommands.SetMeta, "setMeta" },
        { EditorCommands.SetNode, "setNode" },
        { EditorCommands.SetNodeSelection, "setNodeSelection" },
        { EditorCommands.SetParagraph, "setParagraph" },
        { EditorCommands.SetStrike, "setStrike" },
        { EditorCommands.SetTextAlign, "setTextAlign" },
        { EditorCommands.SetTextSelection, "setTextSelection" },
        { EditorCommands.SetUnderline, "setUnderline" },
        { EditorCommands.SinkListItem, "sinkListItem" },
        { EditorCommands.SplitBlock, "splitBlock" },
        { EditorCommands.SplitCell, "splitCell" },
        { EditorCommands.SplitListItem, "splitListItem" },
        { EditorCommands.ToggleBlockquote, "toggleBlockquote" },
        { EditorCommands.ToggleBold, "toggleBold" },
        { EditorCommands.ToggleBulletList, "toggleBulletList" },
        { EditorCommands.ToggleHeaderCell, "toggleHeaderCell" },
        { EditorCommands.ToggleHeaderColumn, "toggleHeaderColumn" },
        { EditorCommands.ToggleHeaderRow, "toggleHeaderRow" },
        { EditorCommands.ToggleHeading, "toggleHeading" },
        { EditorCommands.ToggleHighlight, "toggleHighlight" },
        { EditorCommands.ToggleItalic, "toggleItalic" },
        { EditorCommands.ToggleLink, "toggleLink" },
        { EditorCommands.ToggleList, "toggleList" },
        { EditorCommands.ToggleMark, "toggleMark" },
        { EditorCommands.ToggleNode, "toggleNode" },
        { EditorCommands.ToggleOrderedList, "toggleOrderedList" },
        { EditorCommands.ToggleStrike, "toggleStrike" },
        { EditorCommands.ToggleUnderline, "toggleUnderline" },
        { EditorCommands.ToggleWrap, "toggleWrap" },
        { EditorCommands.Undo, "undo" },
        { EditorCommands.UndoInputRule, "undoInputRule" },
        { EditorCommands.UnsetAllMarks, "unsetAllMarks" },
        { EditorCommands.UnsetBlockquote, "unsetBlockquote" },
        { EditorCommands.UnsetBold, "unsetBold" },
        { EditorCommands.UnsetColor, "unsetColor" },
        { EditorCommands.UnsetHighlight, "unsetHighlight" },
        { EditorCommands.UnsetItalic, "unsetItalic" },
        { EditorCommands.UnsetLink, "unsetLink" },
        { EditorCommands.UnsetMark, "unsetMark" },
        { EditorCommands.UnsetStrike, "unsetStrike" },
        { EditorCommands.UnsetTextAlign, "unsetTextAlign" },
        { EditorCommands.UnsetUnderline, "unsetUnderline" },
        { EditorCommands.UpdateAttributes, "updateAttributes" },
        { EditorCommands.WrapIn, "wrapIn" },
        { EditorCommands.WrapInList, "wrapInList" }
    };

    public static string GetValue(this EditorCommands command)
    {
        return _values[command];
    }
}
