// ======= Funções utilitárias para manipular cookies =======
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift());
    return null;
}

function setCookie(name, value, days = 7) {
    const expires = new Date(Date.now() + days * 864e5).toUTCString();
    document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`;
}

// ======= Manipulação do carrinho =======
function getCart() {
    const cookie = getCookie("semprebella_cart");
    return cookie ? JSON.parse(cookie) : [];
}

function saveCart(cart) {
    setCookie("semprebella_cart", JSON.stringify(cart));
}

function addToCart(product) {
    let cart = getCart();
    let existing = cart.find(x => x.id === product.id);

    if (existing) {
        existing.quantity++;
    } else {
        cart.push({ ...product, quantity: 1 });
    }

    saveCart(cart);
    alert(`✅ ${product.name} foi adicionado ao carrinho!`);
}

function removeFromCart(productId) {
    let cart = getCart().filter(p => p.id !== productId);
    saveCart(cart);
}

function updateQuantity(productId, delta) {
    let cart = getCart();
    let item = cart.find(p => p.id === productId);
    if (!item) return;

    item.quantity = Math.max(1, item.quantity + delta);
    saveCart(cart);
    renderCart();
}

// ======= Renderização do carrinho no modal =======
function renderCart() {
    let cart = getCart();
    let container = document.getElementById("cart-items");
    let totalSpan = document.getElementById("cart-total");
    container.innerHTML = "";
    let total = 0;

    if (cart.length === 0) {
        container.innerHTML = `<p class="text-center text-muted">Carrinho vazio 🛍️</p>`;
        totalSpan.innerText = "R$0,00";
        return;
    }

    cart.forEach(item => {
        let subtotal = item.price * item.quantity;
        total += subtotal;

        container.innerHTML += `
        <div class="d-flex align-items-center mb-3">
            <img src="${item.image}" alt="${item.name}" class="me-2" style="width: 60px; height: 60px; object-fit: cover; border-radius: 8px;">
            <div class="flex-grow-1">
                <strong>${item.name}</strong><br>
                <div class="d-flex align-items-center mt-1">
                    <button class="btn btn-sm btn-outline-secondary me-2" onclick="updateQuantity(${item.id}, -1)">-</button>
                    <span>${item.quantity}</span>
                    <button class="btn btn-sm btn-outline-secondary ms-2" onclick="updateQuantity(${item.id}, 1)">+</button>
                    <span class="ms-auto fw-bold">R$${item.price.toFixed(2)}</span>
                </div>
            </div>
        </div>
        `;
    });

    totalSpan.innerText = "R$" + total.toFixed(2);
}
