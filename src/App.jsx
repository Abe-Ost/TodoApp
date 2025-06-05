import React, { useState, useEffect } from 'react';
import Login from './components/Login';
import TaskForm from './components/TaskForm';
import TaskItem from './components/TaskItem';
import TaskStats from './components/TaskStats';

// Importamos ambos servicios
import * as mockService from './services/mockService';
import * as apiService from './services/apiService';

// Por defecto usamos el mockService, pero podemos cambiarlo a apiService
const service = mockService;

function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [tasks, setTasks] = useState([]);
  const [user, setUser] = useState(null);

  useEffect(() => {
    // Si hay un token en localStorage, cargamos las tareas
    const token = localStorage.getItem('token');
    if (token) {
      setIsAuthenticated(true);
      loadTasks();
    }
  }, []);

  const loadTasks = async () => {
    try {
      const tasksData = await service.getTasks();
      setTasks(tasksData);
    } catch (error) {
      console.error('Error al cargar las tareas:', error);
    }
  };

  const handleLogin = async (success, userData) => {
    setIsAuthenticated(success);
    if (success) {
      setUser(userData);
      await loadTasks();
    }
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    setIsAuthenticated(false);
    setUser(null);
    setTasks([]);
  };

  return (
    <div className="min-h-screen bg-gray-100">
      <div className="container mx-auto px-4 py-8">
        {!isAuthenticated ? (
          <Login onLogin={handleLogin} />
        ) : (
          <div className="space-y-6">
            <div className="flex justify-between items-center">
              <h1 className="text-3xl font-bold text-gray-800">Mi Lista de Tareas</h1>
              <div className="flex items-center gap-4">
                <span className="text-gray-600">
                  Bienvenido, {user?.name || 'Usuario'}
                </span>
                <button
                  onClick={handleLogout}
                  className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700"
                >
                  Cerrar Sesión
                </button>
              </div>
            </div>
            
            <TaskForm setTasks={setTasks} />
            
            <div className="grid gap-4">
              {tasks.map((task) => (
                <TaskItem 
                  key={task.id} 
                  task={task} 
                  setTasks={setTasks}
                />
              ))}
            </div>
            
            <TaskStats tasks={tasks} />
          </div>
        )}
      </div>
    </div>
  );
}

export default App; 