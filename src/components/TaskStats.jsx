import React from 'react';

function TaskStats({ tasks }) {
  const totalTasks = tasks.length;
  const completedTasks = tasks.filter(task => task.completed).length;
  const pendingTasks = totalTasks - completedTasks;

  const priorityCount = tasks.reduce((acc, task) => {
    acc[task.priority] = (acc[task.priority] || 0) + 1;
    return acc;
  }, {});

  return (
    <div className="bg-white p-6 rounded-lg shadow-md">
      <h2 className="text-xl font-semibold mb-4">Estadísticas</h2>
      
      <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
        <div className="bg-blue-50 p-4 rounded-lg">
          <p className="text-sm text-blue-600">Total de Tareas</p>
          <p className="text-2xl font-bold text-blue-800">{totalTasks}</p>
        </div>
        
        <div className="bg-green-50 p-4 rounded-lg">
          <p className="text-sm text-green-600">Completadas</p>
          <p className="text-2xl font-bold text-green-800">{completedTasks}</p>
        </div>
        
        <div className="bg-yellow-50 p-4 rounded-lg">
          <p className="text-sm text-yellow-600">Pendientes</p>
          <p className="text-2xl font-bold text-yellow-800">{pendingTasks}</p>
        </div>
        
        <div className="bg-purple-50 p-4 rounded-lg">
          <p className="text-sm text-purple-600">Progreso</p>
          <p className="text-2xl font-bold text-purple-800">
            {totalTasks ? Math.round((completedTasks / totalTasks) * 100) : 0}%
          </p>
        </div>
      </div>

      <div className="mt-6">
        <h3 className="text-lg font-medium mb-3">Por Prioridad</h3>
        <div className="grid grid-cols-3 gap-4">
          <div className="bg-green-50 p-3 rounded-lg">
            <p className="text-sm text-green-600">Baja</p>
            <p className="text-xl font-bold text-green-800">{priorityCount.baja || 0}</p>
          </div>
          
          <div className="bg-yellow-50 p-3 rounded-lg">
            <p className="text-sm text-yellow-600">Media</p>
            <p className="text-xl font-bold text-yellow-800">{priorityCount.media || 0}</p>
          </div>
          
          <div className="bg-red-50 p-3 rounded-lg">
            <p className="text-sm text-red-600">Alta</p>
            <p className="text-xl font-bold text-red-800">{priorityCount.alta || 0}</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default TaskStats; 