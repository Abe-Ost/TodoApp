import React, { useState } from 'react';
import * as mockService from '../services/mockService';

function TaskItem({ task, setTasks }) {
  const [isEditing, setIsEditing] = useState(false);
  const [editedTask, setEditedTask] = useState(task);

  const priorityColors = {
    baja: 'bg-green-100 text-green-800',
    media: 'bg-yellow-100 text-yellow-800',
    alta: 'bg-red-100 text-red-800'
  };

  const handleUpdate = async () => {
    try {
      const updatedTask = await mockService.updateTask(task.id, editedTask);
      setTasks(prevTasks =>
        prevTasks.map(t => (t.id === task.id ? updatedTask : t))
      );
      setIsEditing(false);
    } catch (error) {
      console.error('Error al actualizar la tarea:', error);
      alert('Error al actualizar la tarea');
    }
  };

  const handleDelete = async () => {
    if (window.confirm('¿Estás seguro de que deseas eliminar esta tarea?')) {
      try {
        await mockService.deleteTask(task.id);
        setTasks(prevTasks => prevTasks.filter(t => t.id !== task.id));
      } catch (error) {
        console.error('Error al eliminar la tarea:', error);
        alert('Error al eliminar la tarea');
      }
    }
  };

  const handleToggleComplete = async () => {
    try {
      const updatedTask = await mockService.toggleTaskStatus(task.id);
      setTasks(prevTasks =>
        prevTasks.map(t => (t.id === task.id ? updatedTask : t))
      );
    } catch (error) {
      console.error('Error al cambiar el estado de la tarea:', error);
      alert('Error al cambiar el estado de la tarea');
    }
  };

  if (isEditing) {
    return (
      <div className="bg-white p-4 rounded-lg shadow space-y-4">
        <input
          type="text"
          value={editedTask.title}
          onChange={e => setEditedTask({ ...editedTask, title: e.target.value })}
          className="w-full p-2 border rounded"
        />
        <textarea
          value={editedTask.description}
          onChange={e => setEditedTask({ ...editedTask, description: e.target.value })}
          className="w-full p-2 border rounded"
          rows="2"
        />
        <div className="flex gap-4">
          <input
            type="date"
            value={editedTask.dueDate}
            onChange={e => setEditedTask({ ...editedTask, dueDate: e.target.value })}
            className="flex-1 p-2 border rounded"
          />
          <select
            value={editedTask.priority}
            onChange={e => setEditedTask({ ...editedTask, priority: e.target.value })}
            className="flex-1 p-2 border rounded"
          >
            <option value="baja">Baja</option>
            <option value="media">Media</option>
            <option value="alta">Alta</option>
          </select>
        </div>
        <div className="flex justify-end gap-2">
          <button
            onClick={() => setIsEditing(false)}
            className="px-4 py-2 text-gray-600 hover:text-gray-800"
          >
            Cancelar
          </button>
          <button
            onClick={handleUpdate}
            className="px-4 py-2 bg-indigo-600 text-white rounded hover:bg-indigo-700"
          >
            Guardar
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="bg-white p-4 rounded-lg shadow">
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-3">
          <input
            type="checkbox"
            checked={task.completed}
            onChange={handleToggleComplete}
            className="h-5 w-5 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500"
          />
          <h3 className={`text-lg font-medium ${task.completed ? 'line-through text-gray-400' : ''}`}>
            {task.title}
          </h3>
        </div>
        <span className={`px-2 py-1 rounded text-sm ${priorityColors[task.priority]}`}>
          {task.priority}
        </span>
      </div>
      <p className={`mt-2 text-gray-600 ${task.completed ? 'line-through' : ''}`}>
        {task.description}
      </p>
      <div className="mt-4 flex items-center justify-between">
        <span className="text-sm text-gray-500">
          Vence: {new Date(task.dueDate).toLocaleDateString()}
        </span>
        <div className="flex gap-2">
          <button
            onClick={() => setIsEditing(true)}
            className="text-indigo-600 hover:text-indigo-800"
          >
            Editar
          </button>
          <button
            onClick={handleDelete}
            className="text-red-600 hover:text-red-800"
          >
            Eliminar
          </button>
        </div>
      </div>
    </div>
  );
}

export default TaskItem; 