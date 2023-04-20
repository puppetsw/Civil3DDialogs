using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace Civil3DUtils;

public sealed class TransactAndForget : IDisposable
{
	private readonly Transaction _transaction;
	private readonly DocumentLock _documentLock;
	private readonly Document _activeDocument;

	/// <summary>
	/// Initializes a new instance of the <see cref="TransactAndForget"/> class.
	/// </summary>
	/// <param name="locked">If set to <c>true</c> the transaction is locked.</param>
	public TransactAndForget(bool locked = false)
	{
		_activeDocument = Application.DocumentManager.MdiActiveDocument;

		if (locked)
			_documentLock = _activeDocument.LockDocument();

		_transaction = _activeDocument.TransactionManager.StartTransaction();
	}

	/// <summary>
	/// Gets and object of type T.
	/// </summary>
	/// <typeparam name="T">The type of <see cref="DBObject"/></typeparam>
	/// <param name="objectId">ObjectId of the object to obtain.</param>
	/// <param name="openMode">Mode to obtain in.</param>
	/// <returns>T.</returns>
	public T GetObject<T>(ObjectId objectId, OpenMode openMode) where T : DBObject
	{
		return (T)_transaction.GetObject(objectId, openMode);
	}

	/// <summary>
	/// Adds the object to the active database.
	/// </summary>
	/// <param name="objectToAdd">The object to add.</param>
	/// <returns><see cref="ObjectId"/>.</returns>
	public ObjectId AddObject(Entity objectToAdd)
	{
		var bt = (BlockTable)_transaction.GetObject(_activeDocument.Database.BlockTableId, OpenMode.ForRead);
		var btr = (BlockTableRecord)_transaction.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

		var objectId = btr.AppendEntity(objectToAdd);
		_transaction.AddNewlyCreatedDBObject(objectToAdd, true);

		btr.DowngradeOpen();

		return objectId;
	}

	/// <inheritdoc />
	public void Dispose()
	{
		_transaction?.Commit();
		_transaction?.Dispose();
		_documentLock?.Dispose();
	}
}
