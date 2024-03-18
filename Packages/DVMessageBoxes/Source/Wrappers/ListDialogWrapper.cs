using DVUnityUtilities;
using Packages.DVMessageBoxes.Source.Dialogs;
using Packages.DVMessageBoxes.Source.Events;
using Packages.DVMessageBoxes.Source.Helpers;
using Packages.DVMessageBoxes.Source.Wrappers.Elements;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.DVMessageBoxes.Source.Wrappers
{
    internal class ListDialogWrapper : DialogWrapper
    {
        [SerializeField] private ListDialogWrapper_Cell _cellTemplate;

        [SerializeField] private TextMeshProUGUI _caption;
        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;

        private List<ListDialogWrapper_Cell> _activeCells = new();

        public override Type GetWrapDialogType()
        {
            return typeof(ListDialog);
        }

        private void OnEnable()
        {
            _cellTemplate.gameObject.SetActive(false);

            if (DialogData is ListDialog dial)
            {
                _caption.text = dial.Caption;

                DialogHelper.SetButtonTextOrDeactivate(_button1, dial.Button1);
                DialogHelper.SetButtonTextOrDeactivate(_button2, dial.Button2);

                InitializeButton(_button1, 0, dial, dial.Act1);
                InitializeButton(_button2, 1, dial, dial.Act2);

                InitializeCells(dial.Values);
            }
        }

        private void InitializeButton(Button button, int buttonId, Dialog dial, Action act)
        {
            button.onClick.AddListener(() =>
            {
                var args = new DialogResponseEventArgs() { DialogButton = buttonId, SelectedItems = GetSelectedItems(), DialogInfo = dial };

                act?.Invoke();
                dial.InvokeInternalRespose(args);
            });
        }

        private List<int> GetSelectedItems()
        {
            var res = new List<int>();

            for (int i = 0; i < _activeCells.Count; i++)
            {
                if (_activeCells[i].IsSelected)
                {
                    res.Add(i);
                }
            }

            return res;
        }

        private void InitializeCells(IEnumerable<string> values)
        {
            ClearCells();

            foreach (var v in values)
            {
                CreateCell(v);
            }
        }

        private void CreateCell(string v)
        {
            var copy = Instantiate(_cellTemplate);

            copy.OnCellClick += Copy_OnCellClick;
            copy.SetText(v);
            copy.transform.SetParent(_cellTemplate.transform.parent, false);
            copy.gameObject.SetActive(true);

            _activeCells.Add(copy);
        }

        private void Copy_OnCellClick(ListDialogWrapper_Cell obj)
        {
            if (DialogData is ListDialog ld)
            {
                if (ld.MultiSelection)
                {
                    obj.SetSelectionStatus(!obj.IsSelected);
                }
                else
                {
                    UnSelectAll();
                    obj.SetSelectionStatus(true);
                }
            }
        }

        private void UnSelectAll()
        {
            _activeCells.ForEach(x => x.SetSelectionStatus(false));
        }

        private void ClearCells()
        {
            _activeCells.DestroyGameObjectsAndClear();
        }
    }
}